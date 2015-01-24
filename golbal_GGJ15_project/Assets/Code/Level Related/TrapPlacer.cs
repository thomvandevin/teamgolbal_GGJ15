using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapPlacer : MonoBehaviour {

    LevelData levelData;
    GameObject trapPrefab;

    public void Initialize(LevelData levelData)
    {
        this.levelData = levelData;
        trapPrefab = Resources.Load("Prefabs/Entities/r_trap") as GameObject;
    }

    public void InstantiateEnemies(GameObject levelPrefab)
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("trapSpawner");
        List<GameObject> trapList = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject newTrap = Instantiate(trapPrefab, spawnPoints[i].transform.position, Quaternion.identity) as GameObject;
            trapList.Add(newTrap);
        }

        levelData._trapList = trapList;
    }
}
