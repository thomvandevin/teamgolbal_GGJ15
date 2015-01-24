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
        //SetLevelData();
    }

    void InitializePlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            playerList.Add(basePlayer);
        }

        //levelData.Initialization(tileOccupied, entityOccupied);

        SetPlayers();
    }

    void SetPlayers()
    {
    //    entityList = new List<GameObject>();

    //    GameObject playerObject = Instantiate(basePlayer, new Vector2(0, 0), Quaternion.identity) as GameObject;

    //    entityList.Add(playerObject);

    //    SetCamera();
    }

    void SetCamera()
    {
        //Camera.main.orthographicSize = tileOccupied.GetLength(1) / 2f;
        //Camera.main.transform.position = new Vector3(tileOccupied.GetLength(0) / 2f - 0.5f, tileOccupied.GetLength(1) / 2f - 0.5f, Camera.main.transform.position.z);

        SetLevelData();
    }

    void SetLevelData()
    {
        levelData = gameObject.AddComponent<LevelData>();
        levelData.Initialize(playerList);
    }
}
