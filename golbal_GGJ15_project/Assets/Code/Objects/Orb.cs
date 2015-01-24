using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

	//public fields
    public bool Attached { get; private set; }
    public Transform Target { get; private set; }
	
	//private fields
    private Animator animator;
    private Vector3 targetOffset;

	//public methods
    public void Attach(Transform target) {
        Target = target;
        Attached = true;
    }

    public void DeAttach() {
        Target = null;
        Attached = false;
        //rigidbody2D.AddForce(new Vector2(Random.Range(-40, 40) * .3f, 8));
    }
	
	//private methods
    private void Awake() {
        Attached = false;
        animator = gameObject.GetComponent<Animator>();
        ObjectController.Get().AddObject(gameObject);
        targetOffset = new Vector3(0, 1, -.1f);
    }

    private void Update() {
        if (Target != null) {
            Move();
        }
    }

    private void Move(){
        if (Vector3.Distance(transform.position, Target.position) > .01f) {
            Vector3 newPosition = Vector3.Lerp(transform.position, Target.position + targetOffset, Time.deltaTime * 30);
            transform.position = newPosition;
        }
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
