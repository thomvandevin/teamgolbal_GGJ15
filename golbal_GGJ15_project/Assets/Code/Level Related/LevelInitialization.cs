using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInitialization : MonoBehaviour {

    LevelData levelData;
    DebuffManager debuffManager;

    List<GameObject> playerList;

    void Start()
    {
        debuffManager = new DebuffManager();
        debuffManager.Initialize();

        levelData = new LevelData();
        levelData.Initialize(debuffManager);

        playerList = new List<GameObject>();
        InitializePlayers();
    }

    void InitializePlayers()
    {
        GameObject basePlayer = Resources.Load("Prefabs/Entities/r_playerBase") as GameObject;

        for (int i = 0; i < 4; i++)
        {
            GameObject newPlayer = Instantiate(basePlayer) as GameObject;
            playerList.Add(newPlayer);
        }

        levelData._playerList = playerList;
    }
}
