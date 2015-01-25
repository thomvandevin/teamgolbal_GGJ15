using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [HideInInspector]

    private GameObject _canvas;
    private GameObject _characterPortrait;
    private List<GameObject> _characterPortraits;

    //private fields
    private static HUDManager _instance;

    //public methods
    public static HUDManager Get()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find("r_HUDConntroller").GetComponent<HUDManager>();
        }
        return _instance;
    }

    void Start()
    {
        _instance = this;
        _canvas = GameObject.Find("Canvas");

        _characterPortraits = new List<GameObject>();
        for (int i = 0; i < PlayerController.Get().players.Count; i++)
        {
            _characterPortraits.Add(GameObject.Instantiate(Resources.Load("Prefabs/UI/t_CharacterPortrait"), _canvas.transform.position, Quaternion.identity) as GameObject);
        }

        for (int i = 0; i < _characterPortraits.Count; i++)
        {
            if (_characterPortraits[i].GetComponent<Image>() != null)
            {
                _characterPortraits[i].GetComponent<Image>().transform.parent = _canvas.transform;

                // AVATARS
                if (i == 0)
                    _characterPortraits[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Entities/Players/red");
                if (i == 1)
                    _characterPortraits[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Entities/Players/blue");
                if (i == 2)
                    _characterPortraits[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Entities/Players/green");
                if (i == 3)
                    _characterPortraits[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Entities/Players/yellow");


                // GAMEJAM MAGIC CODE
                if (_characterPortraits.Count == 1)
                    _characterPortraits[i].GetComponent<Image>().transform.position = new Vector3((380 - (128 * i)) + (256 * i), 64, 0);
                if (_characterPortraits.Count == 2)
                    _characterPortraits[i].GetComponent<Image>().transform.position = new Vector3((320 - (128 * i)) + (256 * i), 64, 0);
                if (_characterPortraits.Count == 3)
                    _characterPortraits[i].GetComponent<Image>().transform.position = new Vector3((240 - (128 * i)) + (256 * i), 64, 0);
                if (_characterPortraits.Count == 4)
                    _characterPortraits[i].GetComponent<Image>().transform.position = new Vector3((190 - (128 * i)) + (256 * i), 64, 0);

                //_characterPortraits[i].GetComponentInChildren<Text>().transform.position = new Vector3(0, 96, 0);
                _characterPortraits[i].GetComponentInChildren<Text>().text = "100%";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
