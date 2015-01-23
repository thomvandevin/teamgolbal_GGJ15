using UnityEngine;
using System.Collections;

public class ColorFlash : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void FlashToColor(Color color, float fadeTime, float delay)
    {
        iTween.ColorTo(gameObject, color, fadeTime);
        Invoke("RestoreColor", delay);        
    }

    public void RestoreColor()
    {
        iTween.ColorTo(gameObject, Color.white, .05f);
    }

    public void RestoreColor(float time)
    {
        iTween.ColorTo(gameObject, Color.white, time);
    }
}
