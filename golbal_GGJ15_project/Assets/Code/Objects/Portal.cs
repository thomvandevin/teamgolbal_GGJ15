using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	//public fields
	
	//private fields
    private Animator anim;
	
	//public methods
    public void TurnOn() {
        anim.SetTrigger("turnOn");
    }

    public void TurnOff() {
        anim.SetTrigger("turnOff");
    }
	
	//private methods
    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void EndGame() {
        Application.LoadLevel("Scene_Ending");
    }
	
}
