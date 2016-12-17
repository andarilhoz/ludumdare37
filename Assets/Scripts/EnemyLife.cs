using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (this.GetComponentInChildren<Enemy>() != null && this.GetComponentInChildren<Enemy>().life <= 0)
        {
            //manager.GetComponent<GameManager>().blocksDinheiro += this.bounty;
            Object.Destroy(this.gameObject);
        } else if (this.GetComponentInChildren<Enemy>() == null) {
            Object.Destroy(this.gameObject);
        }

    }


    public void destroy() {
        Object.Destroy(this.gameObject);
    }
}
