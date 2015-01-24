using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    //static fields
    private static EnemyController _instance;

    //public fields
    public List<Enemy> Enemies;

    //private fields
	
	//public methods

    public static EnemyController Get() {
        if (_instance == null) {
            _instance = GameObject.Find("EnemyController").AddComponent<EnemyController>();
        }
        return _instance;
    }
	
	//private methods
    private void Awake() {
        Enemies = EnemyContainer.Load("Assets/Content/Resources/Data/Enemies.xml").Enemies;
    }
	
}
