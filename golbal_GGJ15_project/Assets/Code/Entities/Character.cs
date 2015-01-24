using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity{

    public GameObject holdingObject { get; private set; }

    private ColorFlash colorFlash;
    private GamePad.Index gamePadIndex;
    private float damageMultiplier;
    private bool isDead;
    private bool isHoldingObject;
    private BoxCollider2D punchCollider;

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
        MaxHealth = 100;
        maxVelocity = new Vector2(10, 7);
        maxKnockback = new Vector2(1, 1);
        isHoldingObject = false;

        punchCollider = transform.FindChild("r_punchCollider").GetComponent<BoxCollider2D>();
        transform.FindChild("r_punchCollider").gameObject.layer = gameObject.layer;
	}

    private void FixedUpdate() {
        if (CanMove) {
            FixedMovement();

            UpdateMove(GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex));
        }
    }

    private void Update() {
        if (CanMove) {
            if (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex)) {
                Attack();
            }
        }
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
            return;
        }

        foreach (GameObject p in (PlayerController.Get().players)) {
            if (p.GetComponent<BoxCollider2D>().bounds.Intersects(punchCollider.bounds) && p != gameObject) {
                p.GetComponent<Character>().Damage(gameObject, Random.Range(10, 13));
            }
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
