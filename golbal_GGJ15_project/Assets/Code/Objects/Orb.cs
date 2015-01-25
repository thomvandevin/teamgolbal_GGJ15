using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

	//public fields
    public Transform Target { get; private set; }
	
	//private fields
    private Animator animator;
    private Vector3 targetOffset;
    private SpriteRenderer shadow;
    private bool updateSortingLayer;

	//public methods
    public void Attach(Transform target) {
        Target = target;
        shadow.enabled = false;
        GetComponent<ResponsiveSortingLayer>().OverrideLayer = true;
        Move();
    }

    public void DeAttach() {
        transform.position = Target.transform.position;
        Target = null;
        shadow.enabled = true;
        GetComponent<ResponsiveSortingLayer>().OverrideLayer = false;
        //rigidbody2D.AddForce(new Vector2(Random.Range(-40, 40), 30));

        GameObject altar = GameObject.FindGameObjectWithTag("End");
        if (altar != null)
        {
            if (Vector2.Distance(transform.position, altar.transform.position) <= 32)
            {
                print("you're done");
            }
        }
    }
	
	//private methods
    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        shadow = transform.FindChild("r_Shadow").GetComponent<SpriteRenderer>();
        ObjectController.Get().AddObject(gameObject);
        targetOffset = new Vector3(0, 1, 0);
    }

    private void Update() {
        if (Target != null) {
            Move();
            UpdateSortingLayer();
        }
    }

    private void Move(){
        if (Vector3.Distance(transform.position, Target.position) > .01f) {
            Vector3 newPosition = Vector3.Lerp(transform.position, Target.position + targetOffset, Time.deltaTime * 60);
            transform.position = newPosition;
        }
    }

    private void UpdateSortingLayer() {
        GetComponent<ResponsiveSortingLayer>().SortingLayer = Target.gameObject.GetComponent<ResponsiveSortingLayer>().SortingLayer + 1;
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
