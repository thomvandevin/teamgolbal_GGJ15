using UnityEngine;
using System.Collections;

public class TrapBehaviour : MonoBehaviour {

    Animator animator;

    int minDamage = 25;
    int maxDamage = 30;

    bool triggered;

	void Start () {
        animator = GetComponent<Animator>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !triggered)
            StartCoroutine(ActivateTrap());
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "player" && !triggered)
        {
            int dealtDamage = Random.Range(minDamage, maxDamage);
            coll.GetComponent<Character>().Damage(gameObject, dealtDamage);

            ActivateTrap();
        }
    }

    IEnumerator ActivateTrap()
    {
        triggered = true;
        animator.SetTrigger("activated");

        yield return new WaitForSeconds(1f);

        triggered = false;
        animator.SetTrigger("deactivated");
    }
}
