using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualElements : MonoBehaviour {
    public Enemy myEnemy;

    public GameObject actualLife;
    
    public GameObject redLife;

    private Vector3 fullLife;

    private float fullLifeX;

    private Camera mCamera;

    private float relativeEnemyLife;
	// Use this for initialization
	void Start () {
        fullLife = actualLife.transform.localScale;
        fullLifeX = actualLife.transform.localScale.x;
        mCamera = GameObject.Find("Camera").GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {
        OnHit();
    }

    public void OnHit() {
        relativeEnemyLife = (myEnemy.life * 100) / myEnemy.topLife;
        fullLife.x = (fullLifeX * relativeEnemyLife) / 100;
        actualLife.transform.localScale = fullLife;
        transform.LookAt(mCamera.transform.position, -Vector3.up);
    }
}
