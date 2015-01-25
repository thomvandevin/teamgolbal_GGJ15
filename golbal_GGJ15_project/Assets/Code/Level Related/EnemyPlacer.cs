using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPlacer : MonoBehaviour {

    LevelData levelData;

    GameObject enemyPrefab;

    public void Initialize(LevelData levelData)
    {
        this.levelData = levelData;

        enemyPrefab = Resources.Load("Prefabs/Entities/Enemy_Skeleton") as GameObject;
    }

    public void InstantiateEnemies(GameObject levelPrefab)
    {
        List<Vector3> spawnPoints = new List<Vector3>();

        foreach (Transform child in levelPrefab.transform)
        {
            if (child.tag == "enemySpawner")
            {
                Vector3 spawnPosition = child.position;
                spawnPoints.Add(spawnPosition);                
            }
        }

        List<GameObject> enemyList = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[i], Quaternion.identity) as GameObject;
            enemyList.Add(newEnemy);
        }

        levelData._enemyList = enemyList;
        Debug.Log("Placed new enemies");
    }
}
