using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    private bool enableScreenShake = true;
    private bool enableColorFlash = true;

    [HideInInspector] public bool IsDead { get; private set; }
    [HideInInspector] public bool CantMove;

    private int maxHealth;
    private int health;
    private Vector2 maxKnockback;
    private ColorFlash colorFlash;

    //public methods
    public void Damage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void Damage(int damage, float damageMultiplier) {
        health -= (int)(damage * damageMultiplier);
        if (health <= 0) {
            Die();
        }
    }
    
    //private methods
	private void Start () {
        maxHealth = 10;
        health = maxHealth;
        CantMove = false;
        maxKnockback = new Vector2(700, 2);

        colorFlash = GetComponent<ColorFlash>();
        if (colorFlash == null) { enableColorFlash = false; }
	
	}

    private void Die() {
        IsDead = true;
    }

    protected void KnockBack(Vector3 source, float multiplier) {
        Vector2 hitDirection = gameObject.transform.position - source;
        hitDirection.Normalize();

        CantMove = true;
        Invoke("FixedMovement", .1f);
        gameObject.rigidbody2D.AddForce(hitDirection * maxKnockback.x * multiplier);

        if (enableScreenShake)
            iTween.PunchPosition(Camera.main.gameObject, -(hitDirection * maxKnockback.y * multiplier), .35f);

        if (enableColorFlash) {
            colorFlash.FlashToColor(new Color(1, 1, 1, 500), .1f, .1f);
        }
    }
}
