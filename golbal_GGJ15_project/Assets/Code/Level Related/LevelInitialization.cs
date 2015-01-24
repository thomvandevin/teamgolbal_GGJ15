using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInitialization : MonoBehaviour {

    LevelData levelData;

    bool[,] tileOccupied;
    bool[,] entityOccupied;

    List<GameObject> entityList;

    GameObject baseTile;
    GameObject basePlayer;

	void Start () {
        levelData = gameObject.AddComponent<LevelData>();

        tileOccupied = new bool[16, 10];
        entityOccupied = new bool[tileOccupied.GetLength(0), tileOccupied.GetLength(1)];

        baseTile = Resources.Load("Prefabs/Tiles/r_baseTile") as GameObject;
        basePlayer = Resources.Load("Prefabs/Entities/r_basePlayer") as GameObject;

        SetTiles();
	}

    void SetTiles()
    {
        for (int xTile = 0; xTile < tileOccupied.GetLength(0); xTile++)
        {
            for (int yTile = 0; yTile < tileOccupied.GetLength(1); yTile++)
            {
                tileOccupied[xTile, yTile] = false;
            }
        }

        PlaceTiles();
    }

    void PlaceTiles()
    {
        GameObject tileParent = new GameObject();
        tileParent.name = "tileParent";

        for (int xTile = 0; xTile < tileOccupied.GetLength(0); xTile++)
        {
            for (int yTile = 0; yTile < tileOccupied.GetLength(1); yTile++)
            {
                if (!tileOccupied[xTile, yTile])
                {
                    GameObject childTile = Instantiate(baseTile, new Vector2(xTile, yTile), Quaternion.identity) as GameObject;
                    childTile.transform.parent = tileParent.transform;
                }
            }
        }

        levelData.Initialization(tileOccupied, entityOccupied);

        SetPlayers();
    }

    void SetPlayers()
    {
        entityList = new List<GameObject>();

        GameObject playerObject = Instantiate(basePlayer, new Vector2(0, 0), Quaternion.identity) as GameObject;
        playerObject.GetComponent<Character>().Initialize(levelData, new Vector2(0, 0));

        entityList.Add(playerObject);

        SetCamera();
    }

    void SetCamera()
    {
        Camera.main.orthographicSize = tileOccupied.GetLength(1) / 2f;
        Camera.main.transform.position = new Vector3(tileOccupied.GetLength(0) / 2f - 0.5f, tileOccupied.GetLength(1) / 2f - 0.5f, Camera.main.transform.position.z);

        SetLevelData();
    }

    void SetLevelData()
    {
        levelData.UpdateEntityList(entityList);
    }
}
