using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public bool canStart() {
        GameObject e1 = Instantiate(enemy1, this.transform.position, Quaternion.identity);
        if (e1.GetComponentInChildren<Enemy>().pathIsClear()){
            Object.Destroy(e1.gameObject);
            return true;
        } else {
            Object.Destroy(e1.gameObject);
            return false;
        }

    }

    public void spawnEnemy(int enemy) {
        switch (enemy) {
            case 1:
                GameObject e1 = Instantiate(enemy1, this.transform.position, Quaternion.identity);
                break;
            case 2:
                GameObject e2 = Instantiate(enemy2, this.transform.position, Quaternion.identity);
                break;
            case 3:
                GameObject e3 = Instantiate(enemy3, this.transform.position, Quaternion.identity);
                break;
            case 4:
                GameObject e4 = Instantiate(enemy4, this.transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("Deu merda bixo");
                break;
        }
        Debug.Log(enemy);        

    }
}
