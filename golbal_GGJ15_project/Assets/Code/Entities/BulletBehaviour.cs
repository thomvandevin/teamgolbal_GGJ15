using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    EnemyBehaviour enemyBehaviour;

	public void Initialize (EnemyBehaviour enemyBehaviour) {
        this.enemyBehaviour = enemyBehaviour;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "PlayerFeet")
        {
            int dealtDamage = Random.Range(15, 30);
            coll.transform.parent.GetComponent<Character>().Damage(gameObject, dealtDamage);
        }
    }

    void RemoveBullet()
    {

    }
}
