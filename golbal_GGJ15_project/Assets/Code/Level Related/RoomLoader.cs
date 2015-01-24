using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomLoader : MonoBehaviour {

    List<GameObject> roomPrefabs;

    public void Initialize()
    {
        roomPrefabs = new List<GameObject>();

        for (int i = 0; i < i + 1; i++)
        {
            string loadingPath = "Prefabs/Rooms/Prefab_Room" + 0;

            if (Resources.Load(loadingPath) != null)
            {
                roomPrefabs.Add(Resources.Load(loadingPath) as GameObject);
            }
        }
    }

    public GameObject GetRandomRoom()
    {
        int randomNumber = Random.Range(0, roomPrefabs.Count);

        return roomPrefabs[randomNumber];
    }
}
