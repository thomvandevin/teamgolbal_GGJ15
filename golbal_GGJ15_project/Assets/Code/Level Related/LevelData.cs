﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : MonoBehaviour {

    RoomLoader roomLoader;    

    MissionCreater.Missions currentMission;

    List<GameObject> playerList;
    List<GameObject> enemyList;
    List<GameObject> trapList;

    List<IslandTemplate> islandList;
    List<GameObject> roomList;

    public List<GameObject> _playerList { get { return playerList; } set { playerList = value; } }
    public List<GameObject> _enemyList { get { return enemyList; } set { enemyList = value; } }
    public List<GameObject> _trapList { get { return trapList; } set { trapList = value; } }

    public List<GameObject> _roomList { get { return roomList; } }

    GameObject roomPrefab;

    bool pvpEnabled;

    public bool _pvpEnable { get { return pvpEnabled; } set { pvpEnabled = value; } }

    public void Initialize()
    {
        roomLoader = new RoomLoader();
        roomLoader.Initialize();

        SetIslands();        
    }

    void SetIslands()
    {
        roomList = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            if (i != 4)
                roomList.Add(roomLoader.GetRandomRoom());
            else
                roomList.Add(roomLoader.GetEndRoom());
        }

        islandList = new List<IslandTemplate>();

        for (int i = 0; i < 5; i++)
        {
            if (i != 4)
            {
                islandList.Add(new IslandTemplate());
                islandList[i].Initialize(IslandTemplate.LevelSelection.Random, this, roomList[i], new Vector2(64 * i, -30 * i));
            }
            else
            {
                islandList.Add(new IslandTemplate());
                islandList[i].Initialize(IslandTemplate.LevelSelection.End, this, roomList[i], new Vector2(64 * i, -30 * i));
            }
        }
    }

    public void SetLevel()
    {
        if (islandList.Count == 1)
            currentMission = MissionCreater.Missions.PlaceOrb;
        else
            CreateMission();

        PlaceMissionItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject enemy = GameObject.Find("r_enemy(Clone)");
            enemy.GetComponent<TestEnemy>().EnemyKilled();
        }
    }

    void SetNextLevel()
    {
        if (islandList.Count > 1)
        {
            islandList.RemoveAt(0);
            roomList.RemoveAt(0);

            Debug.Log(islandList.Count);

            SetLevel();
        }
    }

    void CreateMission()
    {
        MissionCreater missionCreater = new MissionCreater();
        currentMission = missionCreater.CreateMission();
    }

    void JumpToNext()
    {
        MushroomJump mushroomJump = Camera.main.gameObject.AddComponent<MushroomJump>();
        mushroomJump.Initialize(this, playerList, islandList[0]._mushroom, islandList[1]._landingSpot);
    }

    public void StartLevel()
    {
        for (int i = 0; i < playerList.Count; i++)
            SetPlayerMovement(playerList[i], true);

        Destroy(Camera.main.gameObject.GetComponent<MushroomJump>());

        SetNextLevel();
    }

    public void SetPlayerMovement(GameObject playerObject, bool value)
    {
        // playerObject.GetComponent<Character>().moveAble = value;
    }

    void PlaceMissionItems()
    {
        switch (currentMission)
        {
            case MissionCreater.Missions.CommitSuicide:
                SetPvP(false);
                // Place spikes
                break;
            case MissionCreater.Missions.KillCreatures:
                SetPvP();
                islandList[0].StartCreatures();
                break;
            case MissionCreater.Missions.KillPlayer:
                SetPvP();
                break;
            case MissionCreater.Missions.PlaceOrb:
                SetPvP();
                // Place orb altar
                break;
        }
    }

    void RevivePlayers()
    {
        // Loop throught the players, and see if any are a ghost
        // If this happens to be the case, revive them and remove all debuffs
    }

    void PlacePlayers()
    {
        int numberOfGhost = 0;

        for (int i = 0; i < playerList.Count; i++)
        {
            // numberOfghost ++; for every ghost
        }
    }

    void SetPvP(bool pvp = true)
    {
        if (pvpEnabled != pvp)
            pvpEnabled = pvp;
    }

    public void Enemy_Killed(GameObject enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            Destroy(enemy);

            if (CheckMission(MissionCreater.Missions.KillCreatures))
            {
                if (enemyList.Count == 0)
                {
                    JumpToNext();
                }
            }
        }
    }

    public void Player_CommitsSuicide(GameObject player)
    {
        // Award the player who commited suicide

        Player_Died(player);
    }

    public void Player_KillsAnother(GameObject killingPlayer, GameObject killedPlayer)
    {
        // Award the killingPlayer

        Player_Died(killedPlayer);
    }

    public void Player_PlacesOrb(GameObject player)
    {

    }

    public void Player_Died(GameObject player)
    {
        // Set the player to a ghost mode
        // Check to see if all the players have died, and thus went game over
    }

    bool CheckMission(MissionCreater.Missions missionToCheck)
    {
        if (currentMission == missionToCheck)
            return true;
        else
            return false;
    }
}
