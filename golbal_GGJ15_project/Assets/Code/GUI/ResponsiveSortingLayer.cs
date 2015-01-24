using UnityEngine;
using System.Collections;

public class ResponsiveSortingLayer : MonoBehaviour {

    //public fields

    //private fields
    private SpriteRenderer spriteRenderer;
    float sortingY;

    //public methods

    //private methods
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sortingY = spriteRenderer.bounds.min.y * 100;
        spriteRenderer.sortingOrder = Mathf.RoundToInt(sortingY) * -1;
    }

    private void Update() {
        sortingY = spriteRenderer.bounds.min.y * 100;
        spriteRenderer.sortingOrder = Mathf.RoundToInt(sortingY) * -1;
    }

}
