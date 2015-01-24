using UnityEngine;
using System.Collections;

public class IslandTemplate : MonoBehaviour {

    public enum LevelSelection { Random, End };
    LevelSelection levelSelection;

    LevelData levelData;

    EnemyPlacer enemyPlacer;
    TrapPlacer trapPlacer;

    GameObject islandPrefab;
    GameObject islandObject;

    Vector2 islandPosition;

    public void Initialize(LevelSelection levelSelection, LevelData levelData, GameObject islandObject, Vector2 islandPosition)
    {
        this.levelSelection = levelSelection;
        this.levelData = levelData;
        this.islandPrefab = islandObject;
        this.islandPosition = islandPosition;

        InitializeLevel();
    }

    void InitializeLevel()
    {
        enemyPlacer = new EnemyPlacer();
        enemyPlacer.Initialize(levelData);

        trapPlacer = new TrapPlacer();
        trapPlacer.Initialize(levelData);

        islandObject = Instantiate(islandPrefab, islandPosition, Quaternion.identity) as GameObject;
    }

    public void StartCreatures()
    {
        enemyPlacer.InstantiateEnemies(islandObject);
        trapPlacer.InstantiateTraps(islandObject);
    }

    public void StartSuicide()
    {
        // Place suicidal spike
    }

    public void StartOrb()
    {
        // Place the final orb
    }

    public void DeleteLevel()
    {
        levelData._enemyList.Clear();
        levelData._trapList.Clear();
    }
}
