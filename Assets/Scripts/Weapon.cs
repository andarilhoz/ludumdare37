using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private int[] id;

    private GameObject GM;
    public GameObject chao;
    public float fireRate;

    public float range;

    private float fire;

    public GameObject tiro;

    // Use this for initialization
    void Start () {
        GM = GameObject.Find("GameController");
        fire = fireRate;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GameObject[] closestEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 currentPosition = transform.position;
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject potentialTarget in closestEnemy) {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        if ((fire <= 0 && closestEnemy.Length != 0) && (bestTarget.transform.position - currentPosition).sqrMagnitude < this.range) {
            GameObject tiroGerado = Instantiate(tiro, new Vector3(this.transform.position.x , this.transform.position.y + 1,
                                    this.transform.position.z), Quaternion.identity);
            tiroGerado.GetComponent<Bullet>().target = bestTarget.transform;
            fire = fireRate;
        }
        if (closestEnemy.Length != 0 && (bestTarget.transform.position - currentPosition).sqrMagnitude < this.range)
        {
            var targetPos = bestTarget.transform.position;
            targetPos.y = transform.position.y; //set targetPos y equal to mine, so I only look at my own plane
            var targetDir = Quaternion.LookRotation(targetPos - transform.GetChild(0).position);
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, targetDir, 5 * Time.deltaTime);

            //transform.GetChild(0).transform.LookAt(bestTarget.transform.position, transform.up);
        }
        fire--;

    }


    public void setId(int horizontal, int vertical)
    {
        this.id = new int[] { horizontal, vertical };
    }

    public int[] getId() {
        return this.id;
    }

    private void OnMouseUp()
    {
        GM = GameObject.Find("GameController");
        if (GM.GetComponent<GameManager>().getWeapon() == 0) {
            chao.GetComponent<Room>().placeObject(0,this.id[0], this.id[1]);
            Object.Destroy(this.gameObject);
        }
    }


}
