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

    public void InstantiateTraps(GameObject levelPrefab)
    {
        List<GameObject> spawnPoints = new List<GameObject>();

        foreach (Transform child in levelPrefab.transform)
        {
            if (child.tag == "trapSpawner")
                spawnPoints.Add(child.gameObject);
        }

        List<GameObject> trapList = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject newTrap = Instantiate(trapPrefab, spawnPoints[i].transform.position, Quaternion.identity) as GameObject;
            trapList.Add(newTrap);
        }

        levelData._trapList = trapList;
    }
}
