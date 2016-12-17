using System.Collections;
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

    public static int[] active;
    private int[] me;
    // Use this for initialization
    private void Awake()
    {
        for (int arrayNumber = 0; arrayNumber < 5; arrayNumber++)
        {
            for (int objId = 0; objId < 5; objId++)
            {
                chao.GetComponent<Room>().placeObject(0, objId, arrayNumber);
            }
        }
    }

    void Start () {
        ghost1 = GameObject.Find("amoebadoraGhost");
        ghost2 = GameObject.Find("basicGhost");
        ghost3 = GameObject.Find("basicGhost");
        GM = GameObject.Find("GameController");

        me = new int[]{ horizontal, vertical};

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        active = me;
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
        active = new int[] { };
        ghost1.transform.position = new Vector3(-20, -20, -20);
        ghost2.transform.position = new Vector3(-20, -20, -20);
        GetComponent<Renderer>().material = onDefault;
    }

    private void OnMouseUp()
    {
       // Debug.Log("horizontal: " + this.horizontal + " vertical: " + this.vertical);
        switch (GM.GetComponent<GameManager>().getWeapon()) {
            case 0:
                chao.GetComponent<Room>().placeObject(0, active[0], active[1]);
                GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
                foreach (GameObject weapon in weapons) {
                    if (weapon.GetComponent<Weapon>().getId()[0] == active[0] && weapon.GetComponent<Weapon>().getId()[1] == active[1]) {
                        Object.Destroy(weapon.gameObject);
                    }
                }
                break;
            case 1:
                chao.GetComponent<Room>().placeObject(1, active[0], active[1]);
                break;
            case 2:
                chao.GetComponent<Room>().placeObject(2, active[0], active[1]);
                break;
            case 3:
                chao.GetComponent<Room>().placeObject(3, active[0], active[1]);
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
