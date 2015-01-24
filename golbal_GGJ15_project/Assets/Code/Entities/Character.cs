using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity{

    private ColorFlash colorFlash;

    private GamePad.Index gamePadIndex;
    private float damageMultiplier;
    private bool isDead;
    private bool isHoldingObject;
    private GameObject holdingObject;

	private void Awake () 
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
        if (!isHoldingObject) {
            foreach (GameObject o in (ObjectController.Get().objects)) {
                if (o.GetComponent<CircleCollider2D>().bounds.Intersects(GetComponent<BoxCollider2D>().bounds)) {
                    PickUpObject(o);
                    return;
                }
            }
        } else {
            DropObject(holdingObject);
        }
        
    }

    private void OnDestroy() {
        if (isHoldingObject) {
            //dropobject
        }
        PlayerController.Get().RemovePlayer(gameObject);
    }

    private void PickUpObject(GameObject o) {
        o.GetComponent<Orb>().Attach(gameObject.transform);
        holdingObject = o;
        isHoldingObject = true;
    }

    private void DropObject(GameObject o) {
        o.GetComponent<Orb>().DeAttach();
        holdingObject = null;
        isHoldingObject = false;
    }

}
