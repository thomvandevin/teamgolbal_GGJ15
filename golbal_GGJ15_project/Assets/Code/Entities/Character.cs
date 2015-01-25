﻿using UnityEngine;
using System.Collections;
using GamepadInput;

public class Character : Entity {

    public GameObject holdingObject { get; private set; }

    private ColorFlash colorFlash;
    private GamePad.Index gamePadIndex;
    private float damageMultiplier;
    private bool isDead;
    private bool isHoldingObject;
    private BoxCollider2D punchCollider;
    private Animator animator;
    private string currentAnimation;
    private bool alreadyAnimating;

    private void Awake() {
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
        animator = GetComponent<Animator>();
        currentAnimation = "";
        alreadyAnimating = false;
        OnDamage = Hit;

        punchCollider = transform.FindChild("r_punchCollider").GetComponent<BoxCollider2D>();
        transform.FindChild("r_punchCollider").gameObject.layer = gameObject.layer;
    }

    private void FixedUpdate() {
        if (CanMove) {
            FixedMovement();
            Vector2 axisValues = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);
            UpdateMove(axisValues);
        }
    }

    private void Update() {
        if (CanMove) {
            if (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex)) {
                Attack();
            }

            if (isHoldingObject) {
                SetAnimationBool("holdingObject", true);
            }

            if (!alreadyAnimating) {
                Vector2 axisValues = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);
                if (axisValues != Vector2.zero) {
                    SetAnimation("Run");
                } else {
                    SetAnimation("Idle");

                }
            }
        }

        print(isHoldingObject);

        if (alreadyAnimating && animator.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation)) {
            alreadyAnimating = false;
        }
    }

    private void Attack() {
        if (!isHoldingObject) {
            foreach (GameObject o in (ObjectController.Get().objects)) {
                if (o.GetComponent<CircleCollider2D>().bounds.Intersects(GetComponent<BoxCollider2D>().bounds)) {
                    PickUpObject(o);
                    SetAnimationBool("holdingObject", true);
                    return;
                }
            }
        } else {
            DropObject();
            SetAnimationBool("holdingObject", false);
            return;
        }

        foreach (GameObject p in (PlayerController.Get().players)) {
            if (p.GetComponent<BoxCollider2D>().bounds.Intersects(punchCollider.bounds) && p != gameObject) {
                p.GetComponent<Character>().Damage(gameObject, Random.Range(10, 13));
            }
        }

        if (!alreadyAnimating) {
            SetAnimation("Attack");
            //alreadyAnimating = true;
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

    private void DropObject() {
        isHoldingObject = false;
        holdingObject.GetComponent<Orb>().DeAttach();
        holdingObject = null;
    }

    private void Hit() {
        if (isHoldingObject) {
            DropObject();
            SetAnimationBool("holdingObject", false);
        }
    }

    private void SetAnimation(string animation) {
        currentAnimation = animation;
        animator.SetTrigger(animation);
    }

    private void SetAnimationBool(string animation, bool value) {
        animator.SetBool(animation, value);
    }

}
