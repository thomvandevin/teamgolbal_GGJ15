using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity{

    private ColorFlash colorFlash;

    private GamePad.Index gamePadIndex;
    private float damageMultiplier;
    private bool isDead;
    private bool isHoldingObject;

	private void Awake () 
    {
        base.Awake();

        CameraFollow.Get().Register(gameObject.transform);
        PlayerController.Get().AddPlayer(gameObject);

        gamePadIndex = GamePad.Index.One;
        gameObject.layer = 7 + GamePad.GetIntFromIndex(gamePadIndex);
        Direction = Facing.Right;
        damageMultiplier = 1f;
        MaxHealth = 10;
        maxVelocity = new Vector2(10, 7);
        maxKnockback = new Vector2(1, 1);
        isHoldingObject = false;
	}

    private void Update() {
        if (CanMove) {
            UpdateMove(GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex));
            if (GamePad.GetButtonDown(GamePad.Button.X, gamePadIndex)) {
                Attack();
            }
        }


        if (GamePad.GetKeyboardKeyDown(KeyCode.LeftShift))
            KnockBack(new Vector2(0, 0), damageMultiplier);
    }

    private void Attack() {
        foreach(GameObject o in (ObjectController.Get().objects)){
            if(o.GetComponent<CircleCollider2D>().bounds.Intersects(GetComponent<BoxCollider2D>().bounds)){
                if (!isHoldingObject) {
                    PickUpObject(o);
                }
                return;
            }
        }
    }

    private void OnDestroy() {
        PlayerController.Get().RemovePlayer(gameObject);
    }

    private void PickUpObject(GameObject o) {
        o.GetComponent<Orb>().Attach(gameObject.transform);
        isHoldingObject = true;
    }

    private void DropObject(GameObject o) {
        o.GetComponent<Orb>().DeAttach();
        isHoldingObject = false;
    }

}
