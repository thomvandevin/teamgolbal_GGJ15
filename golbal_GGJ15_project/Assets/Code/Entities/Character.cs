using UnityEngine;
using System.Collections;
using GamepadInput;

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

    private void Awake()
    {
        base.Awake();

        CameraFollow.Get().Register(gameObject.transform);
        PlayerController.Get().AddPlayer(gameObject);

        int playerIndexInt = PlayerController.Get().players.IndexOf(gameObject) + 1;
        gamePadIndex = GamePad.GetIndexFromInt(playerIndexInt);
        gameObject.layer = 7 + playerIndexInt;
        gameObject.name = "Player " + playerIndexInt;
        Direction = Facing.Right;
        damageMultiplier = 1f;
        MaxHealth = 100;
        maxVelocity = new Vector2(10, 7);
        maxKnockback = new Vector2(700, 700);
        isHoldingObject = false;
        animator = GetComponent<Animator>();
        currentAnimation = "";
        alreadyAnimating = false;
        OnDamage += Hit;

        punchCollider = transform.FindChild("r_punchCollider").GetComponent<BoxCollider2D>();
        transform.FindChild("r_punchCollider").gameObject.layer = gameObject.layer;
    }

    private void FixedUpdate()
    {
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
    }

    private void Attack()
    {
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

}
