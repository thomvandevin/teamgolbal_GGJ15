using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : MonoBehaviour {

    public bool enableScreenShake = true;
    public bool enableScreenFlash = false;

    [HideInInspector]
    private enum Facing
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    private Facing Direction;

    private Animator top_animator, bottom_animator;
    private GameObject mainCamera;

    private ColorFlash colorFlash;

    private GamePad.Index gamePadIndex;
    private Vector2 move, maxVelocity, maxKnockback;
    private int health, maxHealth;
    private float damageMultiplier;
    private bool isDead;
    private bool dontMove;

	void Start () 
    {
        top_animator = Global.getChildGameObject(gameObject, "Animation_top").GetComponent<Animator>();
        bottom_animator = Global.getChildGameObject(gameObject, "Animation_bottom").GetComponent<Animator>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        colorFlash = GetComponent<ColorFlash>();

        gamePadIndex = GamePad.Index.One;
        Direction = Facing.RIGHT;
        move = Vector2.zero;
        maxVelocity = new Vector2(5, 3);
        maxKnockback = new Vector2(700, 2 );
        damageMultiplier = 1f;
        dontMove = false;

        maxHealth = 10;
        health = maxHealth;
        isDead = false;

	}
	
    void FixedUpdate()
    {
        if(!dontMove)
            FixedMovement();
    }

	void Update () 
    {
        Movement();

        if (GamePad.GetKeyboardKeyDown(KeyCode.LeftShift))
            KnockBack(new Vector2(0, 0), damageMultiplier);

        if (GamePad.GetKeyboardKeyDown(KeyCode.Space))
            Dash();
	}

    private void FixedMovement()
    {
        if (dontMove)
            dontMove = false;

        move = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);

        if (GamePad.GetKeyboardKey(KeyCode.LeftArrow))
            move = new Vector2(-1, move.y);
        else if (GamePad.GetKeyboardKey(KeyCode.RightArrow))
            move = new Vector2(1, move.y);

        if (GamePad.GetKeyboardKey(KeyCode.UpArrow))
            move = new Vector2(move.x, 1);
        else if (GamePad.GetKeyboardKey(KeyCode.DownArrow))
            move = new Vector2(move.x, -1);

        SetAnimation("hSpeed", Mathf.Abs(move.x));
        SetAnimation("vSpeed", Mathf.Abs(move.y));
        rigidbody2D.velocity = new Vector2(move.x * maxVelocity.x, move.y * maxVelocity.x);

        if (move.x > 0 && Direction == Facing.LEFT)
            Flip();
        else if (move.x < 0 && Direction == Facing.RIGHT)
            Flip();
    }

    private void Movement()
    {

    }

    private void Dash()
    {
        dontMove = true;
        Invoke("FixedMovement", .075f);
        gameObject.rigidbody2D.AddForce(move.normalized * maxVelocity.x * 75);

        if(enableScreenFlash)
            mainCamera.GetComponent<ScreenFlash>().Flash(.05f);

    }

    private void Damage(GameObject source, int damage, float multiplier)
    {
        if(!isDead)
        {
            health -= Mathf.RoundToInt(damage * multiplier);
            KnockBack(source.transform.position, multiplier);
        }

        if (health <= 0)
            isDead = true;
    }

    private void KnockBack(Vector3 source, float multiplier)
    {
        Vector2 hitDirection = gameObject.transform.position - source;
        hitDirection.Normalize();

        dontMove = true;
        Invoke("FixedMovement", .1f);
        gameObject.rigidbody2D.AddForce(hitDirection * maxKnockback.x * multiplier);

        if(enableScreenShake)
            iTween.PunchPosition(mainCamera, -(hitDirection * maxKnockback.y * multiplier), .35f);

        colorFlash.FlashToColor(new Color(1, 1, 1, 500), .1f, .1f);
    }

    private void Flip()
    {
        if (Direction == Facing.LEFT)
            Direction = Facing.RIGHT;
        else if (Direction == Facing.RIGHT)
            Direction = Facing.LEFT;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

#region SET ANIMATION STUFF

    private void SetAnimation(string name, int value)
    {
        top_animator.SetInteger(name, value);
        bottom_animator.SetInteger(name, value);
    }
    private void SetAnimation(string name, float value)
    {
        top_animator.SetFloat(name, value);
        bottom_animator.SetFloat(name, value);
    }
    private void SetAnimation(string name, bool value)
    {
        top_animator.SetBool(name, value);
        bottom_animator.SetBool(name, value);
    }

#endregion
    
}
