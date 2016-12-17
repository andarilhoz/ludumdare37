using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject fastFowardButton;

    public GameObject playPause;

    public Sprite pauseImage;
    public Sprite playImage;


    private int actualTurn;
    private bool turnActive;

    private int activeWeapon;

    public int turns;
    public int enemyPerTurn;
    public int minInterval;
    public int maxInterval;
    public int vidas;
    public int blocksDinheiro;
    public EnemySpawn spawn;
    public Room room;

    public Text waves;
    public TextAsset jsonFile;
    public Text blocksText;
    public Text lifeText;
    public Waves[] waveList = new Waves[8];

 
    public bool pause;
    public bool doingtutorial = true;

    public GameObject gameOver;
    public GameObject gameWin;
    public GameObject gamePause;
    public GameObject tutorial;
    public GameObject  blocked;

    private int fastFowardActive = 0;
    [System.Serializable]
    private class WaveList
    {
        public List<Waves> waves;
    }

    private bool generating = false;

    // Use this for initialization
    void Start () {
        room.hideSpots();
        tutorial.SetActive(true);
        this.blocksDinheiro = 99;//4;
        this.vidas = 20;
        this.actualTurn = 0;
        this.turnActive = false;
        this.activeWeapon = 2;
        Time.timeScale = 1;
        //spawn.spawnEnemy();


        WaveList wl = new WaveList();
        JsonUtility.FromJsonOverwrite(jsonFile.text, wl);
        waveList = wl.waves.ToArray();



    }
	
	// Update is called once per frame
	void Update () {
        waves.text = "WAVE " + this.actualTurn;
        blocksText.text = ""+this.blocksDinheiro;
        lifeText.text = ""+this.vidas;
        if ((vidas > 0  && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) && (this.turnActive && !this.generating)) {
            this.blocksDinheiro += waveList[this.actualTurn - 1].bounty;
            if(fastFowardActive > 0)
                Time.timeScale = 1;
            this.fastFowardActive = 0;
            fastFowardButton.GetComponent<Text>().text = "FASTFOWARD 1X";
            this.stopTurn();
        };
        if (this.turnActive && !this.pause)
        {
            playPause.GetComponent<Image>().sprite = pauseImage;
        }
        else {
            playPause.GetComponent<Image>().sprite = playImage;
        }
        if (vidas <= 0) {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        if (this.actualTurn > 10) {
            gameWin.SetActive(true);
            Time.timeScale = 0;
        }

        if (pause)
        {
            Time.timeScale = 0;
            if (Input.GetKey("escape"))
                Application.Quit();
        }
        else if(vidas > 0 && !(this.fastFowardActive > 0)) {
            Time.timeScale = 1;
        }
    }

    int getActualTurn() {
        return this.actualTurn;
    }

    public void fastFoward() {
        if (this.turnActive && this.fastFowardActive == 0  && this.fastFowardActive < 2)
        {
            Debug.Log("Acelera");
            Time.timeScale = 2;
            this.fastFowardActive = 2;
            fastFowardButton.GetComponent<Text>().text = "FASTFOWARD 2X";
        } else if(this.turnActive && this.fastFowardActive > 1 && this.fastFowardActive < 3)
        {
            Debug.Log("Celeeera");
            Time.timeScale = 4;
            this.fastFowardActive = 4;
            fastFowardButton.GetComponent<Text>().text = "FASTFOWARD 4X";
        }
        else if (this.turnActive && this.fastFowardActive > 3 && this.fastFowardActive < 5)
        {
            Debug.Log("Celeeera");
            Time.timeScale = 6;
            this.fastFowardActive = 6;
            fastFowardButton.GetComponent<Text>().text = "FASTFOWARD 6X";
        }
        else if(!this.pause && (this.turnActive && (this.fastFowardActive > 0))){
            Time.timeScale = 1;
            this.fastFowardActive = 0;
            fastFowardButton.GetComponent<Text>().text = "FASTFOWARD 1X";
        }
    }

    private int getEnemyType(string type)
    {
        switch (type)
        {
            case "soldier":
                return 3;
            case "doll":
                return 1;
            case "car":
                return 2;
            case "boss":
                return 4;
            default:
                Debug.Log("Deu merda bixo");
                return 0;
        }
    }

    IEnumerator showTextFuntion(int[] quantity, string[] types)
    {
        for (int index = 0; index < types.Length; index++)
        {
            for (int q = 0; q < quantity[index]; q++)
            {
                generating = true;
                Debug.Log("oi");
                spawn.spawnEnemy(getEnemyType(types[index]));
                yield return new WaitForSeconds(3f);
                Debug.Log("Tchau");
                generating = false;
            }

        }
    }

    public void clickMenu(string option) {
        switch (option) {
            case "exit":
                Application.Quit();
                break;
            case "retry":
                Time.timeScale = 1;
                SceneManager.LoadScene("MainGame");
                break;
            case "closetutorial":
                doingtutorial = false;
                tutorial.SetActive(false);
                room.showSpots();
                break;
            case "closeblocked":
                blocked.SetActive(false);
                break;
            default:
                break;
        }
    }


    public void nextTurn() {

        if (spawn.canStart() && !this.turnActive)
        {
            Debug.Log("chegou");
            Waves w = waveList[this.actualTurn];
            StartCoroutine(showTextFuntion(w.quantity, w.type));
            this.actualTurn++;
            room.hideSpots();
            this.turnActive = true;
            this.pause = false;
        }
        else if (this.turnActive && !this.pause)
        {
            gamePause.SetActive(true);
            playPause.GetComponent<Image>().sprite = playImage;
            Time.timeScale = 0;
            this.pause = true;
        } else if (this.turnActive && this.pause)
        {
            Debug.Log("Despausando");
            Time.timeScale = 1;
            this.pause = false;
            gamePause.SetActive(false);
        } else if (!spawn.canStart()) {
            blocked.SetActive(true);
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
