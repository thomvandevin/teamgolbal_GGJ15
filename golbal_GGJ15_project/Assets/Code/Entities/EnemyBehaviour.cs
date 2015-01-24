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
        foreach (Enemy e in EnemyController.Get().Enemies) {
            if (e.type == type) {
                MaxHealth = e.health;
                speed = e.speed;
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
