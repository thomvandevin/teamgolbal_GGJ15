using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomLoader : MonoBehaviour {

    List<GameObject> roomPrefabs;

    public void Initialize()
    {
        roomPrefabs = new List<GameObject>();
        int roomNumber = 0;

        while (true)
        {
            string loadingPath = "Prefabs/Levels/Prefab_Room" + roomNumber.ToString();

            GameObject newPrefab = Resources.Load(loadingPath) as GameObject;

            if (newPrefab != null)
                roomPrefabs.Add(newPrefab);
            else
                break;

            roomNumber++;
        }
    }

    public GameObject GetRandomRoom()
    {
        int randomNumber = Random.Range(0, roomPrefabs.Count);

        Debug.Log(roomPrefabs.Count);

        return roomPrefabs[randomNumber];
    }

    public GameObject GetEndRoom()
    {
        return Resources.Load("Prefabs/Levels/Prefab_FinalRoom") as GameObject;
    }
}
