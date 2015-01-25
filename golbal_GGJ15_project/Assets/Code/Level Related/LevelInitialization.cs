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

        //playerList = new List<GameObject>();
        //InitializePlayers();
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
