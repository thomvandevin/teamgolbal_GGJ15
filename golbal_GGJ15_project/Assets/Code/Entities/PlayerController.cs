using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	//public fields
    [HideInInspector]
    public List<GameObject> players;
	
	//private fields
    private static PlayerController _instance;
	
	//public methods
    public static PlayerController Get() {
        if (_instance == null) {
            _instance = GameObject.Find("r_ObjectController").AddComponent<PlayerController>();
        }
        return _instance;
    }

    public void AddPlayer(GameObject player) {
        players.Add(player);
    }

    public void RemovePlayer(GameObject player) {
        players.Remove(player);
    }
	
	//private methods
    private void Awake() {
        _instance = this;
        players = new List<GameObject>();
    }
	
}
