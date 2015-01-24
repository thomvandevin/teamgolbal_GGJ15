using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPlacer : MonoBehaviour {

    LevelData levelData;

    GameObject enemyPrefab;

    public void Initialize(LevelData levelData)
    {
        this.levelData = levelData;

        enemyPrefab = Resources.Load("Prefabs/Entities/r_enemy") as GameObject;
    }

    public void InstantiateEnemies(GameObject levelPrefab)
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("enemySpawner");
        List<GameObject> enemyList = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[i].transform.position, Quaternion.identity) as GameObject;
            enemyList.Add(newEnemy);
        }

        levelData._enemyList = enemyList;
    }
}
