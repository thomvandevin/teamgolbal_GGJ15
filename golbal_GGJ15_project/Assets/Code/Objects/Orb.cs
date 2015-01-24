using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

	//public fields
    public bool Attached { get; private set; }
	
	//private fields
    private Animator animator;

	//public methods
    public void Attach(Transform target) {
        transform.parent = target;
        gameObject.MoveTo(transform.position, 3f, 0f);
        Attached = true;
    }

    public void DeAttach() {
        transform.parent = null;
        rigidbody2D.AddForce(new Vector2(Random.Range(-1, 1) * .3f, .3f));
    }
	
	//private methods
    private void Awake() {
        Attached = false;
        animator = gameObject.GetComponent<Animator>();
        ObjectController.Get().AddObject(gameObject);
        TriggerFluctuation();
    }

    private void OnDestroy() {
        ObjectController.Get().RemoveObject(gameObject);
    }

    private void TriggerFluctuation() {
        TriggerAnimation("fluctuate");
    }

    private void TriggerAnimation(string animstring) {
        animator.SetTrigger(animstring);
        int randomInvoke = Random.Range(50, 100);
        Invoke("TriggerFluctuation", randomInvoke);
    }
}
