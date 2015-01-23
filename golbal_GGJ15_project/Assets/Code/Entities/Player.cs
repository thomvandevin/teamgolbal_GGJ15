using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    
    [HideInInspector]

    public Character characterScript;


	void Start () 
    {
        characterScript = this.gameObject.AddComponent<Character>();
	}
	
	void Update () 
    {
	
	}
}
