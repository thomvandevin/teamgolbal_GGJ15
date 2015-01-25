using UnityEngine;
using System.Collections;

public class SortingLayerParent : MonoBehaviour {

	//public fields
    public GameObject Target;
    public int LayerOffset;
	
	//private fields
    private ResponsiveSortingLayer sortingLayer;
    private ResponsiveSortingLayer parentSortingLayer;
	
	//public methods
	
	//private methods
    private void Awake() {
        if (Target == null) {
            Target = gameObject;
        }
        sortingLayer = GetComponent<ResponsiveSortingLayer>();
        sortingLayer.OverrideLayer = true;
        parentSortingLayer = Target.GetComponent<ResponsiveSortingLayer>();
    }

    private void Update() {
        sortingLayer.SortingLayer =  parentSortingLayer.SortingLayer + LayerOffset;
    }
	
}
