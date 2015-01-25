using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInitialization : MonoBehaviour {

    LevelData levelData;

    List<GameObject> playerList;

    void Start()
    {
        levelData = gameObject.AddComponent<LevelData>();        
        levelData.Initialize();

        InitializePlayers();
    }

    void InitializePlayers()
    {
        GameObject basePlayer = Resources.Load("Prefabs/Entities/prefab_tempPlayer") as GameObject;

        playerList = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            GameObject newPlayer = Instantiate(basePlayer) as GameObject;
            playerList.Add(newPlayer);
        }

        levelData._playerList = playerList;
        levelData.SetLevel();
    }
}
