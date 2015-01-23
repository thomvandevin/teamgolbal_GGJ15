using UnityEngine;
using System.Collections;

public class ScreenFlash : MonoBehaviour {

    public Color flashColor;

    private GUITexture flash;
    private Texture2D texture;
    private GameObject storage;

	// Use this for initialization
	void Start () 
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();

        storage = new GameObject("Flash");
        storage.transform.localPosition = new Vector3(.5f, .5f, 0);
        storage.transform.localScale = new Vector3(1, 1, 1);

        flash = storage.AddComponent<GUITexture>();
        flash.texture = texture; 
        flash.color = Color.white;
        flash.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Flash(float duration)
    {
        flash.enabled = true;
        Invoke("Cancel", duration);
    }

    public void SetFlashColor(Color color)
    {
        flash.color = color;
    }

    public void Cancel()
    {
        flash.enabled = false;
    }
}
