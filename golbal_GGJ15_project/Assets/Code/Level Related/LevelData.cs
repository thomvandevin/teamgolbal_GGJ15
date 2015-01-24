﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : MonoBehaviour {

    MissionCreater.Missions goalMission;
    RoomLoader roomLoader;

    List<GameObject> playerList;
    List<GameObject> enemyList;
    List<GameObject> trapList;

    GameObject enemyPrefab, trapPrefab;
    GameObject roomPrefab;

    bool pvpEnabled;

    public void Initialize(List<GameObject> playerList)
    {
        this.playerList = playerList;

        roomLoader = new RoomLoader();
        roomLoader.Initialize();

        UpdatePlayers();

        Reset();
    }

    void UpdatePlayers()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject playerObject = Instantiate(playerList[i]) as GameObject;
            //playerObject.GetComponent<Character>() Set level data
        }
    }

    void Reset()
    {
        enemyList.Clear();
        trapList.Clear();

        LoadRoom();
        CreateMission();
        PlaceMissionItems();

        RevivePlayers();
        PlacePlayers();
    }

    void LoadRoom()
    {
        if (GameObject.Find("currentRoom") != null)
            Destroy(GameObject.Find("currentRoom"));

        roomPrefab = roomLoader.GetRandomRoom();

        GameObject newRoom = Instantiate(roomPrefab) as GameObject;
        newRoom.name = "currentRoom";
    }

    void CreateMission()
    {
        MissionCreater missionCreater = new MissionCreater();
        goalMission = missionCreater.CreateMission();
    }

    void PlaceMissionItems()
    {
        switch (goalMission)
        {
            case MissionCreater.Missions.CommitSuicide:
                SetPvP(false);
                // Place spikes
                break;
            case MissionCreater.Missions.KillCreatures:
                SetPvP();
                // Spawn monsters
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
                    // The players have won the mission
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
        if (goalMission == missionToCheck)
            return true;
        else
            return false;
    }
}
