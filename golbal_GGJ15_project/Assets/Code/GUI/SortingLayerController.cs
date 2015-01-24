using UnityEngine;
using System.Collections;

public class SortingLayerController : MonoBehaviour {

	//public fields
	
	//private fields
	
	//public methods
	
	//private methods
    private void Awake() {
        SpriteRenderer[] sprites = GameObject.FindObjectsOfType<SpriteRenderer>() as SpriteRenderer[];
        foreach (SpriteRenderer sprite in sprites) {
            if (sprite.sortingLayerName == "") {
                sprite.gameObject.AddComponent<ResponsiveSortingLayer>();   
            }         
        }
    }
	
}
