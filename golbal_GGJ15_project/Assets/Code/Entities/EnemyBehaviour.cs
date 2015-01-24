using UnityEngine;
using System.Collections;

public enum EnemyType {
    Melee,
    Ranged,
}

public class EnemyBehaviour : Entity {

	//public fields
    public EnemyType type;

    [HideInInspector]
    public Transform Target;

	//private fields
    private Animator animator;

    private enum AnimationState {
        Walk,
        Run,
        Idle,
        Attack
    }
	
	//public methods
	
	//private methods
    private void Awake() {
        base.Awake();

        foreach (Enemy e in EnemyController.Get().Enemies) {
            if (e.type == type) {
                MaxHealth = e.health;
                maxVelocity = new Vector2(e.maxvelocityx, e.maxvelocityy);
                print(maxVelocity);
                maxKnockback = new Vector2(500, 2);
            }
        }

        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Target != null) {
            //movee towards player.
        }

    }

    private void TriggerAnimation(AnimationState state) {
        animator.SetTrigger(state.ToString());
    }
	
}
