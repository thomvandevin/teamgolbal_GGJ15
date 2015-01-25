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
        GameObject target = new GameObject();
        target.transform.position = Target.transform.position - new Vector3(0, 1, 0);
        Target = target.transform;
        GetComponent<ResponsiveSortingLayer>().OverrideLayer = false;
        Invoke("NoTarget", .1f);
        //rigidbody2D.AddForce(new Vector2(Random.Range(-40, 40), 30));

        if (GameObject.FindGameObjectWithTag("End") != null) {
            if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("End").transform.position) <= 32) {
                print("you're done");
            }
        }
    }

    private void NoTarget() {
        Target = null;
        shadow.enabled = true;
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
            if (updateSortingLayer)
                UpdateSortingLayer();
        }
    }

    private void Move() {
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
