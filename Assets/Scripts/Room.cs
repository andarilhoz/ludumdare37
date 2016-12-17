using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour {
    public GameObject WeaponPlace;

    public GameObject Arma1;
    public GameObject Arma2;
    public GameObject Arma3;


    private GameObject[] spots;
    private static GameObject GM;

    private int[,] espacos = 
        {
            { 0,0,0,0,0},
            { 0,0,0,0,0},
            { 0,0,0,0,0},
            { 0,0,0,0,0},
            { 0,0,0,0,0}
        };


    // Use this for initialization
    void Start() {

        GM = GameObject.Find("GameController");
        for (int arrayNumber = 0; arrayNumber < 5; arrayNumber++)
        {
            for (int objId = 0; objId < 5; objId++)
            {
                GameObject spot = Instantiate(WeaponPlace,
                    new Vector3(this.transform.position.x + (5 * arrayNumber) - 10, this.transform.position.y + 1,
                                this.transform.position.z + (5 * objId) - 10), Quaternion.identity);
                spot.GetComponent<WeaponSpot>().setId(arrayNumber, objId);
            }
        }

        spots = GameObject.FindGameObjectsWithTag("Spot");
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void placeObject(int objectId, int horizontal, int vertical)
    {
        if (this.espacos[horizontal, vertical] == 0)
        {
            switch (objectId)
            {
                case 0:
                    this.removeObject(horizontal, vertical);
                    break;
                case 1:
                    if (GM.GetComponent<GameManager>().blocksDinheiro >= 5)
                    {
                        GameObject arma1 = Instantiate(Arma1, new Vector3(this.transform.position.x + (5 * horizontal) - 10, this.transform.position.y + 0.6f,
                                    this.transform.position.z + (5 * vertical) - 10), Quaternion.identity);
                        arma1.GetComponent<Weapon>().setId(horizontal, vertical);
                        this.espacos[horizontal, vertical] = objectId;
                        GM.GetComponent<GameManager>().blocksDinheiro -= 5;
                    }
                    break;
                case 2:
                    if (GM.GetComponent<GameManager>().blocksDinheiro >= 3)
                    {
                        GameObject arma2 = Instantiate(Arma2, new Vector3(this.transform.position.x + (5 * horizontal) - 10, this.transform.position.y + 0.6f,
                                    this.transform.position.z + (5 * vertical) - 10), Quaternion.identity);
                        arma2.GetComponent<Weapon>().setId(horizontal, vertical);
                        this.espacos[horizontal, vertical] = objectId;
                        GM.GetComponent<GameManager>().blocksDinheiro -= 3;
                    }
                    break;
                case 3:
                    GameObject arma3 = Instantiate(Arma3, new Vector3(this.transform.position.x + (5 * horizontal) - 10, this.transform.position.y + 0.6f,
                                this.transform.position.z + (5 * vertical) - 10), Quaternion.identity);
                    arma3.GetComponent<Weapon>().setId(horizontal, vertical);
                    break;
                default:
                    break;
            }
        }
        else {
            if (objectId == 0) {
                this.removeObject(horizontal, vertical);
            }
        }
    }

    int getObjectId(int horizontal, int vertical) {
        return this.espacos[horizontal, vertical];
    }

    public void removeObject(int horizontal, int vertical) {
        if (GameObject.FindGameObjectsWithTag("Weapon").Length == 1 && GM.GetComponent<GameManager>().blocksDinheiro < 3)
        {
            GM.GetComponent<GameManager>().blocksDinheiro = 3;
        }
        else
        {
            switch (this.espacos[horizontal, vertical])
            {
                case 1:
                    GM.GetComponent<GameManager>().blocksDinheiro += 3;
                    break;
                case 2:
                    GM.GetComponent<GameManager>().blocksDinheiro += 1;
                    break;
                case 3:
                    GM.GetComponent<GameManager>().blocksDinheiro += 0;
                    break;
                default:
                    break;
            }
        }
        this.espacos[horizontal, vertical] = 0;
    }

    public void hideSpots() {
        

        foreach (GameObject spot in spots) {
            spot.SetActive(false);
        }
    }

    public void showSpots()
    {
        foreach (GameObject spot in spots)
        {
            spot.SetActive(true);
        }
    }
}
