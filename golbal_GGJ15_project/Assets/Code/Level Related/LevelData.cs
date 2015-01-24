using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : MonoBehaviour {

    List<GameObject> entityList;

    bool[,] tileOccupied;
    bool[,] entityOccupied;

    public List<GameObject> _entityList { get { return entityList; } }
    public bool[,] _tileOccupied { get { return tileOccupied; } }
    public bool[,] _entityOccupied { get { return entityOccupied; } }

    public void Initialization(bool[,] tileOccupied, bool[,] entityOccupied)
    {
        this.tileOccupied = tileOccupied;
        this.entityOccupied = entityOccupied;

        Debug.Log("Leveldata: Added");
    }

    public void AddEntity(GameObject entity)
    {
        entityList.Add(entity);
    }

    public void UpdateEntityList(List<GameObject> entityList)
    {
        this.entityList = entityList;
    }

    public void SetTileOccupied(Vector2 tilePosition, bool tileValue)
    {
        tileOccupied[(int)tilePosition.x, (int)tilePosition.y] = tileValue;
    }

    public void SetEntityOccupied(Vector2 entityPosition, bool entityValue)
    {
        entityOccupied[(int)entityPosition.x, (int)entityPosition.y] = entityValue;
    }
}
