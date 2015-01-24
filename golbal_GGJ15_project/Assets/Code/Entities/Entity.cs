using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    protected LevelData levelData;

    protected Vector2 entityPosition;

    protected float speed = 0.2f;
    protected bool moving;

    protected void Initialize(LevelData levelData)
    {
        this.levelData = levelData;

        levelData.SetEntityOccupied(entityPosition, true);


    }

    protected void CheckPosition(Vector2 moveDirection)
    {
        Vector2 futurePosition = entityPosition + moveDirection;

        if (!CheckIfOccupied(futurePosition)) // We know the position isn't occupied
            StartCoroutine(MovePlayer(entityPosition, futurePosition));
    }

    protected IEnumerator MovePlayer(Vector2 oldPosition, Vector2 newPosition)
    {
        levelData.SetEntityOccupied(newPosition, true);
        moving = true;

        float timer = 0;
        bool resetPosition = false;

        while (true)
        {
            timer += (1f / speed) * Time.deltaTime;
            transform.position = Vector2.Lerp(oldPosition, newPosition, timer);

            if (timer > 0.8f && !resetPosition)
            {
                levelData.SetEntityOccupied(oldPosition, false);
                resetPosition = true;
            }

            if(timer >= 1)
                break;

            yield return null;
        }

        entityPosition = newPosition;
        transform.position = entityPosition;

        moving = false;
    }

    protected bool CheckIfOccupied(Vector2 checkPosition)
    {
        if (checkPosition.x < 0 || checkPosition.y < 0 || checkPosition.x >= levelData._tileOccupied.GetLength(0) || checkPosition.y >= levelData._tileOccupied.GetLength(1))
            return true;

        if (levelData._tileOccupied[(int)checkPosition.x, (int)checkPosition.y])
            return true;

        if (levelData._entityOccupied[(int)checkPosition.x, (int)checkPosition.y])
            return true;

        return false;
    }
}
