﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        if (closestEnemy.Length != 0)
        {
            transform.GetChild(0).transform.LookAt(bestTarget.transform.position, transform.up);
        }

    }




}
