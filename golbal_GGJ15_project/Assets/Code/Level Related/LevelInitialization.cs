using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInitialization : MonoBehaviour {

    LevelData levelData;

    List<GameObject> playerList;

    GameObject basePlayer;

    void Start()
    {
        // basePlayer = Resources.Load("Prefabs/Entities/r_playerBase") as GameObject;

        InitializePlayers();
        SetLevelData();
    }

    void InitializePlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            playerList.Add(basePlayer);
        }
    }

    void SetLevelData()
    {
        levelData = gameObject.AddComponent<LevelData>();
        levelData.Initialize(playerList);
    }
}
