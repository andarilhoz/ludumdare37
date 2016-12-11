﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    private int actualTurn;
    private bool turnActive;

    private int activeWeapon;

    public int turns;
    public int enemyPerTurn;
    public int minInterval;
    public int maxInterval;
    public EnemySpawn spawn;
    public Room room;

    // Use this for initialization
    void Start () {
        this.actualTurn = 0;
        this.turnActive = false;
        this.activeWeapon = 2;
        //spawn.spawnEnemy();	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && this.turnActive) {
            this.stopTurn();
        };
    }

    int getActualTurn() {
        return this.actualTurn;
    }

    public void nextTurn() {
        if (spawn.spawnEnemy())
        {
            this.actualTurn++;
            room.hideSpots();
            this.turnActive = true;
        }
        else {
            Debug.Log("Path is blocked");
        }

    }

    void stopTurn() {
        this.turnActive = false;
        room.showSpots();
    }

    public void changeWeapon(int weapon) {
        this.activeWeapon = weapon;
    }

    public int getWeapon() {
        return this.activeWeapon;
    }

}
