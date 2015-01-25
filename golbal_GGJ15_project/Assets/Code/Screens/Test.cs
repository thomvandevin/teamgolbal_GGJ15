using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	//public fields
	
	//private fields
	
	//public methods
	
	//private methods
    void OnGUI() {
        if (GUI.Button(new Rect(50, 50, 80, 40), "Test")) {
            rigidbody2D.AddForce(Vector2.right * 400);
        }
    }
	
}

