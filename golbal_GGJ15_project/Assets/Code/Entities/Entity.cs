using UnityEngine;
using System.Collections;
using GamepadInput;
using System;

public class Entity : MonoBehaviour {

    [HideInInspector]
    public enum Facing {
        Left,
        Right,
        Up,
        Down
    }
    protected Facing Direction;

    protected Action OnDamage;

    //public fields
    protected int MaxHealth;
    [HideInInspector]
    public int Health { get; private set; }
    [HideInInspector]
    public bool IsDead { get; private set; }

    protected bool CanMove;

    //private fields
    protected Vector2 move, maxVelocity, maxKnockback;
    private bool playerControl;

    //public methods
    public void KnockBack(Vector3 source, float multiplier) {
        Vector2 hitDirection = gameObject.transform.position - source;
        hitDirection.Normalize();

        CanMove = false;
        Invoke("FixedMovement", .3f);
        gameObject.rigidbody2D.AddForce(hitDirection * maxKnockback.x * multiplier * UnityEngine.Random.Range(1,1.3f));
        iTween.PunchPosition(Camera.main.gameObject, -(hitDirection * multiplier * UnityEngine.Random.Range(1, 1.3f)), .2f);
    }

    public void Damage(GameObject source, int damageValue) {
        Health -= damageValue;
        KnockBack(source.transform.position, 1);
        Hit();
        if (Health <= 0)
            Death();
    }

    public void Damage(GameObject source, int damageValue, float damageMultiplier) {
        Health -= Mathf.RoundToInt(damageValue * damageMultiplier);
        KnockBack(source.transform.position, damageMultiplier);
        Hit();
        if (Health <= 0)
            Death();
    }

    private void Hit() {
    }

    private void Death() {
        
    }

    protected void UpdateMove(Vector2 directions) {
        if (playerControl) {
            move = directions;
        }
    }

    //private methods
    protected void Awake() {
        move = Vector2.zero;
        IsDead = false;
        CanMove = true;
        playerControl = true;
        OnDamage = Hit;
    }

    protected void FixedMovement() {

        if (!CanMove)
            CanMove = true;

        move.Normalize();
        rigidbody2D.velocity = new Vector2(move.x * maxVelocity.x, move.y * maxVelocity.y);

        if (move.x < 0 && Direction == Facing.Left)
            Flip();
        else if (move.x > 0 && Direction == Facing.Right)
            Flip();


    }

    private void Flip() {
        if (Direction == Facing.Left)
            Direction = Facing.Right;
        else if (Direction == Facing.Right)
            Direction = Facing.Left;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

}
