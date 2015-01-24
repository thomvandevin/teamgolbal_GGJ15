using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    //public fields
    [HideInInspector]
    public List<Enemy> Enemies;

    //private fields
    private static EnemyController _instance;
	
	//public methods

    public static EnemyController Get() {
        if (_instance == null) {
            _instance = GameObject.Find("r_EnemyController").AddComponent<EnemyController>();
        }
        return _instance;
    }
	
	//private methods
    private void Awake() {
        _instance = this;
        Enemies = EnemyContainer.Load("Assets/Content/Resources/Data/Enemies.xml").Enemies;
    }
	
}
