using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectController : MonoBehaviour {

	//public fields
    [HideInInspector]
    public List<GameObject> objects;
	
	//private fields
    private static ObjectController _instance;

	//public methods
    public static ObjectController Get() {
        if (_instance == null) {
            _instance = GameObject.Find("r_ObjectController").GetComponent<ObjectController>();
        }
        return _instance;
    }

    public void AddObject(GameObject gobject) {
        objects.Add(gobject);
    }

    public void RemoveObject(GameObject gobject) {
        objects.Remove(gobject);
    }
	
	//private methods
    private void Awake() {
        _instance = this;
        objects = new List<GameObject>();
    }

}
