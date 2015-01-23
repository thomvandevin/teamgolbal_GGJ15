using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity {

    [HideInInspector]
    private enum Facing
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    private Facing Direction;
    
    private GamePad.Index gamePadIndex;
    private Vector2 move, maxVelocity;

	private void Start () 
    {
        gamePadIndex = GamePad.Index.One;
        Direction = Facing.RIGHT;
        move = Vector2.zero;
        maxVelocity = new Vector2(5, 3);
	}
	
    private void FixedUpdate()
    {
        if(!CantMove)
            FixedMovement();
    }

	private void Update () 
    {
        if (GamePad.GetKeyboardKeyDown(KeyCode.LeftShift))
            KnockBack(new Vector2(0, 0), 1);

        if (GamePad.GetKeyboardKeyDown(KeyCode.Space))
            Dash();
	}

    private void FixedMovement()
    {
        if (CantMove) { CantMove = false; }

        move = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);

        if (GamePad.GetKeyboardKey(KeyCode.LeftArrow))
            move = new Vector2(-1, move.y);
        else if (GamePad.GetKeyboardKey(KeyCode.RightArrow))
            move = new Vector2(1, move.y);

        if (GamePad.GetKeyboardKey(KeyCode.UpArrow))
            move = new Vector2(move.x, 1);
        else if (GamePad.GetKeyboardKey(KeyCode.DownArrow))
            move = new Vector2(move.x, -1);

        rigidbody2D.velocity = new Vector2(move.x * maxVelocity.x, move.y * maxVelocity.x);

        if (move.x > 0 && Direction == Facing.LEFT)
            Flip();
        else if (move.x < 0 && Direction == Facing.RIGHT)
            Flip();
    }

    private void Dash()
    {
        CantMove = true;
        Invoke("FixedMovement", .075f);
        gameObject.rigidbody2D.AddForce(move.normalized * maxVelocity.x * 75);
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
    
}
