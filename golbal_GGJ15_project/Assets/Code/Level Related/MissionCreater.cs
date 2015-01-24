using UnityEngine;
using System.Collections;

public class MissionCreater : MonoBehaviour {

    public enum Missions { CommitSuicide, KillPlayer, KillCreatures, PlaceOrb };
    Missions missions;

    int numberOfLevels;

    float mission_Suicide = 15f;
    float mission_KillPlayer = 30f;
    float mission_KillCreature = 100f;

    public Missions CreateMission()
    {
        RandomMission();
        numberOfLevels++;

        return missions;
    }

    void RandomMission()
    {
        if (numberOfLevels < 5)
        {
            int randomValue = Random.Range(1, 101);

            if (randomValue <= mission_Suicide)
                missions = Missions.CommitSuicide;
            else
                if (randomValue <= mission_KillPlayer)
                    missions = Missions.KillPlayer;
                else
                    missions = Missions.KillCreatures;
        }
        else
        {
            missions = Missions.PlaceOrb;
        }
    }
}
