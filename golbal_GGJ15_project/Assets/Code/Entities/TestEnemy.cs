using UnityEngine;
using System.Collections;

public class TestEnemy : MonoBehaviour {

    LevelData levelData;

    public void Initialize(LevelData levelData)
    {
        this.levelData = levelData;
    }

    public void EnemyKilled()
    {
        levelData.Enemy_Killed(gameObject);
    }
}
