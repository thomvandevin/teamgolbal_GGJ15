using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity{


    private ColorFlash colorFlash;

    private GamePad.Index gamePadIndex;
    private float damageMultiplier;
    private bool isDead;

	void Start () 
    {
        gamePadIndex = GamePad.Index.One;
        Direction = Facing.Right;

        damageMultiplier = 1f;
        MaxHealth = 10;

        maxVelocity = new Vector2(10, 5);
        maxKnockback = new Vector2(1, 1);
	}

    private void Update() {
        if (CanMove) {
            UpdateMove(GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex));
        }


        if (GamePad.GetKeyboardKeyDown(KeyCode.LeftShift))
            KnockBack(new Vector2(0, 0), damageMultiplier);
    }

}
