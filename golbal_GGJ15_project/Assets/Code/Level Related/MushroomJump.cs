using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MushroomJump : MonoBehaviour {

    LevelData levelData;

    List<GameObject> playersList;
    bool[] playersLanded;

    GameObject startPosition, endPosition;

    int numberPlayersLanded;
    
    public void Initialize(LevelData levelData, List<GameObject> playersList, GameObject startPosition, GameObject endPosition)
    {
        this.levelData = levelData;
        this.playersList = playersList;
        this.startPosition = startPosition;
        this.endPosition = endPosition;

        playersLanded = new bool[playersList.Count];

        MoveObjects();
    }

    void MoveObjects()
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            StartCoroutine(MoveToMushroom(playersList[i]));
        }
    }

    IEnumerator MoveToMushroom(GameObject movedObject)
    {
        levelData.SetPlayerMovement(movedObject, false);

        Vector3 lerpStart = movedObject.transform.position;

        float timer = 0f;
        float lerpTime = 0.5f + ((Vector3.Distance(lerpStart, startPosition.transform.position)) / 15f);

        while(true)
        {
            timer += 1f * Time.deltaTime;
            float lerpPosition = timer / lerpTime;

            movedObject.transform.position = Vector3.Lerp(lerpStart, startPosition.transform.position, lerpPosition);

            if (lerpPosition >= 1)
                break;

            yield return null;
        }

        movedObject.transform.position = startPosition.transform.position;

        StartCoroutine(JumpMushroom(movedObject));
    }

    IEnumerator JumpMushroom(GameObject movedObject)
    {
        Vector3 lerpStart = movedObject.transform.position;

        Vector3 handleA = new Vector3(startPosition.transform.position.x + ((endPosition.transform.position.x - startPosition.transform.position.x)), startPosition.transform.position.y + 10);
        Vector3 handleB = new Vector3(startPosition.transform.position.x + ((endPosition.transform.position.x - startPosition.transform.position.x)), endPosition.transform.position.y + 10);

        float timer = 0f;
        float lerpTime = 3f;

        while (true)
        {
            timer += 1f * Time.deltaTime;
            float lerpPosition = timer / lerpTime;

            movedObject.transform.position = Bezier.CalculateBezierPoint(lerpPosition, lerpStart, handleA, handleB, endPosition.transform.position);

            if (lerpPosition >= 1)
                break;

            yield return null;
        }

        movedObject.transform.position = endPosition.transform.position;
        SetLanded();
    }

    void SetLanded()
    {
        numberPlayersLanded++;

        if (numberPlayersLanded >= playersLanded.Length)
        {
            levelData.StartLevel();
        }
    }
}
