using UnityEngine;
using System.Collections;

public class ResponsiveSortingLayer : MonoBehaviour {

    //public fields
    public int SortingLayer;
    public bool OverrideLayer = false;

    //private fields
    private SpriteRenderer spriteRenderer;
    float sortingY;

    //public methods

    //private methods
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = HandleSorting();
    }

    private int HandleSorting() {
        if (!OverrideLayer) {
            sortingY = spriteRenderer.bounds.min.y * 100;
            SortingLayer = Mathf.RoundToInt(sortingY) * -1;
        }

        return SortingLayer;
    }

    private void Update() {
        spriteRenderer.sortingOrder = HandleSorting();
    }

}
