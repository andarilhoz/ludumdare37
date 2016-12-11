﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpot : MonoBehaviour {
    public Material mouseOver;
    public Material onDefault;
    public GameObject chao;
    private GameObject GM;

    private GameObject ghost1;
    private GameObject ghost2;
    private GameObject ghost3;

    private int horizontal;
    private int vertical;
	// Use this for initialization
	void Start () {
        ghost1 = GameObject.Find("amoebadoraGhost");
        ghost2 = GameObject.Find("basicGhost");
        GM = GameObject.Find("GameController");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        switch (GM.GetComponent<GameManager>().getWeapon()) {
            case 0:
                break;
            case 1:
                ghost1.transform.position = this.transform.position;
                break;
            case 2:
                ghost2.transform.position = this.transform.position;
                break;
            case 3:
                ghost3.transform.position = this.transform.position;
                break;
            default:
                Debug.Log("Deu merda bixo");
                break;
        }
        
        GetComponent<Renderer>().material = mouseOver;
    }

    private void OnMouseExit()
    {
        ghost1.transform.position = new Vector3(-20, -20, -20);
        ghost2.transform.position = new Vector3(-20, -20, -20);
        GetComponent<Renderer>().material = onDefault;
    }

    private void OnMouseDown()
    {
        Debug.Log("horizontal: " + this.horizontal + " vertical: " + this.vertical);
        switch (GM.GetComponent<GameManager>().getWeapon()) {
            case 1:
                chao.GetComponent<Room>().placeObject(1, this.horizontal, this.vertical);
                break;
            case 2:
                chao.GetComponent<Room>().placeObject(2, this.horizontal, this.vertical);
                break;
            case 3:
                chao.GetComponent<Room>().placeObject(3, this.horizontal, this.vertical);
                break;
            default:
                Debug.Log("Deu merda bixo");
                break;
        }
    }

    public void setId(int h,int v) {
        this.horizontal = h;
        this.vertical = v;
    }

}
