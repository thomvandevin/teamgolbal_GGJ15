using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType {
    Melee,
    Ranged,
}

public class EnemyBehaviour : Entity {

    LevelData levelData;

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

    GameObject enemyBullet;
    List<GameObject> bulletList;
	
	//public methods
	
	//private methods
    private void Awake() {
        base.Awake();

        foreach (Enemy e in EnemyController.Get().Enemies) {
            if (e.type == type) {
                MaxHealth = e.health;
                maxVelocity = new Vector2(e.maxvelocityx, e.maxvelocityy);
                maxKnockback = new Vector2(500, 2);
            }
        }

        animator = GetComponent<Animator>();

        bulletList = new List<GameObject>();
        enemyBullet = Resources.Load("Prefabs/enemyBullet") as GameObject;
    }

    public void Initialize(LevelData levelData)
    {
        this.levelData = levelData;

        StartCoroutine(ShootAtPlayer(Random.Range(2f, 5f)));
    }

    IEnumerator ShootAtPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        GameObject playerTarget = levelData._playerList[0];

        GameObject newBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity) as GameObject;
        newBullet.GetComponent<BulletBehaviour>().Initialize(this);

        Vector2 dir = playerTarget.transform.position - transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90f;
        newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bulletList.Add(newBullet);

        StartCoroutine(ShootAtPlayer(Random.Range(3f, 7f)));
    }

    private void Update() {

    }

    private void FixedUpdate()
    {
        if (bulletList.Count > 0)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].transform.Translate(new Vector3(0, 5, 0) * Time.deltaTime);

                float bulletDistance = Vector2.Distance(bulletList[i].transform.position, transform.position);

                if (bulletDistance >= 20f)
                {
                    RemoveBullet(bulletList[i]);
                }
            }
        }
    }

    public void RemoveBullet(GameObject bullet)
    {
        bulletList.Remove(bullet);
        Destroy(bullet);
    }

    private void TriggerAnimation(AnimationState state) {
        animator.SetTrigger(state.ToString());
    }
	
}
