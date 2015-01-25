using UnityEngine;
using System.Collections;
using GamepadInput;
using UnityEngine.UI;

public class Character : Entity
{

    public GameObject holdingObject { get; private set; }

    public int AdditiveSpeed { get { return AdditiveSpeed; } set { maxVelocity *= AdditiveSpeed; } }
    public int AdditiveHealth { get { return AdditiveHealth; } set { MaxHealth += AdditiveHealth; } }
    public int AdditiveAttack { get { return AdditiveAttack; } set { damageMultiplier = AdditiveAttack; } }

    private ColorFlash colorFlash;
    private GamePad.Index gamePadIndex;
    private float damageMultiplier;
    private bool isDead;
    private BoxCollider2D punchCollider;
    private Animator animator;
    private string currentAnimation;
    private bool alreadyAnimating;

    public Image healthBar;

    private void Awake()
    {
        base.Awake();

        CameraFollow.Get().Register(gameObject.transform);
        PlayerController.Get().AddPlayer(gameObject);

        int playerIndexInt = PlayerController.Get().players.IndexOf(gameObject) + 1;
        gamePadIndex = GamePad.GetIndexFromInt(playerIndexInt);
        gameObject.layer = 7 + playerIndexInt;
        gameObject.transform.FindChild("r_feet").gameObject.layer = gameObject.layer;
        gameObject.name = "Player " + playerIndexInt;
        Direction = Facing.Right;
        damageMultiplier = 1f;
        MaxHealth = 100;
        Health = MaxHealth;
        maxVelocity = new Vector2(10, 7);
        maxKnockback = new Vector2(700, 700);
        isHoldingObject = false;
        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = GetCharacterAnimator(playerIndexInt);
        currentAnimation = "";
        alreadyAnimating = false;
        OnDamage += Hit;

        punchCollider = transform.FindChild("r_punchCollider").GetComponent<BoxCollider2D>();
        transform.FindChild("r_punchCollider").gameObject.layer = gameObject.layer;

        healthBar = null;
    }

    private void FixedUpdate()
    {
        if (IsDead)
            return;
        if (CanMove)
        {
            FixedMovement();
            Vector2 axisValues = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);
            UpdateMove(axisValues);
        }
    }

    private void Update()
    {

        //Vector2 camSize = new Vector2(camera.orthographicSize * Screen.width / Screen.height, camera.orthographicSize);
        //if (transform.position.x < camSize.x || transform.position.y < camSize.y)
        //{
        //    if (transform.position.x > 1920 || transform.position.y > 1080)
        //        CanMove = false;
        //}
        //else
        //{
        //    CanMove = !isDead;
        //}
        if (CanMove)
        {
            if (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex))
            {
                Attack();
            }

            //if (isHoldingObject)
            //{
            //    SetAnimationBool("holdingObject", true);
            //}

            if (!alreadyAnimating)
            {
                Vector2 axisValues = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);
                if (axisValues != Vector2.zero)
                {
                    SetAnimation("Run");
                }
                else
                {
                    SetAnimation("Idle");

                }
            }
        }

        if (alreadyAnimating && animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {
            alreadyAnimating = false;
        }

        if (healthBar)
            healthBar.rectTransform.sizeDelta = new Vector2(Health * 0.71f, 11);

        if (IsDead && !animator.GetCurrentAnimatorStateInfo(0).IsName("Anim_Player_Blue_Death"))
        {
            SetAnimation("Death");
            return;
        }
    }

    private void Attack()
    {
        if (IsDead)
            return;
        if (!isHoldingObject)
        {
            foreach (GameObject o in (ObjectController.Get().objects))
            {
                if (o.GetComponent<CircleCollider2D>().bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
                {
                    PickUpObject(o);
                    return;
                }
            }
        }
        else
        {
            DropObject();
            return;
        }
        if (!isHoldingObject)
        {
            foreach (GameObject p in (PlayerController.Get().players))
            {
                if (p.GetComponent<BoxCollider2D>().bounds.Intersects(punchCollider.bounds) && p != gameObject)
                {
                    p.GetComponent<Character>().Damage(gameObject, Random.Range(10, 13), damageMultiplier);
                }
            }
        }
        if (!alreadyAnimating)
        {
            SetAnimation("Attack");
            //alreadyAnimating = true;
        }
    }

    private void OnDestroy()
    {
        if (isHoldingObject)
        {
            DropObject();
        }
        OnDamage -= Hit;
        PlayerController.Get().RemovePlayer(gameObject);
    }

    private void PickUpObject(GameObject o)
    {
        if (IsDead)
            return;
        if (!isHoldingObject)
        {
            o.GetComponent<Orb>().Attach(gameObject.transform, gamePadIndex);
            if (o.GetComponent<Orb>().HeldByPlayer == gamePadIndex)
            {
                holdingObject = o;
                isHoldingObject = true;
                SetAnimationBool("holdingObject", true);
            }
        }
    }

    private void DropObject()
    {
        if (isHoldingObject)
        {
            isHoldingObject = false;
            holdingObject.GetComponent<Orb>().DeAttach();
            holdingObject = null;
            SetAnimationBool("holdingObject", false);
        }
    }

    private void Hit()
    {
        if (isHoldingObject)
        {
            DropObject();
            print("test");
            SetAnimationBool("holdingObject", false);
        }
    }

    private void SetAnimation(string animation)
    {
        currentAnimation = animation;
        animator.SetTrigger(animation);
    }

    private void SetAnimationBool(string animation, bool value)
    {
        animator.SetBool(animation, value);
    }

    private RuntimeAnimatorController GetCharacterAnimator(int playerIndex)
    {
        RuntimeAnimatorController controller;
        switch (playerIndex)
        {
            case 1:
                return controller = Resources.Load("Sprites/Entities/Players/Animations/Red/Animator_Player_Red") as RuntimeAnimatorController;
            case 2:
                return controller = Resources.Load("Sprites/Entities/Players/Animations/Blue/Animator_Player_Blue") as RuntimeAnimatorController;
            case 3:
                return controller = Resources.Load("Sprites/Entities/Players/Animations/Green/Animator_Player_Green") as RuntimeAnimatorController;
            case 4:
                return controller = Resources.Load("Sprites/Entities/Players/Animations/Yellow/Animator_Player_Yellow") as RuntimeAnimatorController;
            default:
                return controller = Resources.Load("Sprites/Entities/Players/Animations/Red/Animator_Player_Red") as RuntimeAnimatorController;
        }

    }
}
