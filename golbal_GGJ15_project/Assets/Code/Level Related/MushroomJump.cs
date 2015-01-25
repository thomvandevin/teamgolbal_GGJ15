using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MushroomJump : MonoBehaviour {

    List<GameObject> playersList;

    GameObject startPosition, endPosition;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("trapSpawner");

        List<GameObject> newList = new List<GameObject>();

        for (int i = 0; i < players.Length; i++)
        {
            newList.Add(players[i]);
        }

        GameObject startObject = GameObject.Find("Mushroom");
        GameObject endObject = GameObject.Find("LandPad");

        Initialize(newList, startObject, endObject);
    }

    public void Initialize(List<GameObject> playersList, GameObject startPosition, GameObject endPosition)
    {
        this.playersList = playersList;
        this.startPosition = startPosition;
        this.endPosition = endPosition;

        MoveObjects();
    }

    void SetPlayers(GameObject playerObject, bool value)
    {
        //playerObject.GetComponent<Character>().moveAble = value;
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
        SetPlayers(movedObject, false);

        Vector3 lerpStart = movedObject.transform.position;

        float timer = 0f;
        float lerpTime = 0.5f + (Vector3.Distance(lerpStart, startPosition.transform.position));

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

        Vector3 handleA = new Vector3((endPosition.transform.position.x - startPosition.transform.position.x) / 4f, startPosition.transform.position.y + 10);
        Vector3 handleB = new Vector3((endPosition.transform.position.x - startPosition.transform.position.x) / 4f * 3, endPosition.transform.position.y + 10);

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

        SetPlayers(movedObject, true);
    }
}
