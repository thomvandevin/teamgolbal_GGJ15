using UnityEngine;
using System.Collections;
using GamepadInput;

public class Orb : MonoBehaviour {

    //public fields
    public Transform Target { get; private set; }
    public GamePad.Index HeldByPlayer { get; private set; }

    //private fields
    private Animator animator;
    private Vector3 targetOffset;
    private SpriteRenderer shadow;
    private bool updateSortingLayer;
    private CircleCollider2D collider;

    private bool isFalling;
    private Vector2 previousPosition;

    //public methods
    public void Attach(Transform target, GamePad.Index playerIndex) {
        if (isFalling)
            return;
        Target = target;
        shadow.enabled = false;
        GetComponent<ResponsiveSortingLayer>().OverrideLayer = true;
        collider.enabled = false;
        isFalling = false;
        HeldByPlayer = playerIndex;
        //Move();
    }

    public void DeAttach() {
        GameObject target = new GameObject();
        target.transform.position = Target.transform.position - new Vector3(0, 1, 0);
        Target = target.transform;
        GetComponent<ResponsiveSortingLayer>().OverrideLayer = false;
        collider.enabled = true;
        Invoke("NoTarget", .1f);
        Destroy(target);
        Vector2 hitDirection = new Vector2(transform.position.x, transform.position.y) - previousPosition;
        hitDirection.Normalize();

        rigidbody2D.AddForce(new Vector2(Random.Range(-500, 500), Random.Range(-400, 400)));
        //rigidbody2D.AddForce(hitDirection.normalized * Random.Range(400, 500));

        if (GameObject.FindGameObjectWithTag("End") != null) {
            if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("End").transform.position) <= 1) {
                print("you're done");
            }
        }
        //isFalling = true;
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
        collider = GetComponent<CircleCollider2D>();
        targetOffset = new Vector3(0, 1, 0);
    }

    private void Update() {
        if (Target != null) {
            Move();
            if (updateSortingLayer)
                UpdateSortingLayer();
        }
        if (isFalling) {
            isFalling = !rigidbody2D.IsSleeping();
        }
    }

    private void LateUpdate() {
        Vector2 transPos = new Vector2(transform.position.x, transform.position.y);
        if (transPos != previousPosition)
            previousPosition = new Vector2(transform.position.x, transform.position.y);
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
