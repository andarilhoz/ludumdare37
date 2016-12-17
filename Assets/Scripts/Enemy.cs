using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    private GameObject objetivo;
    private GameManager manager;
    private float speed;

    public int dano;
    public int bounty;
    public int life;
    public int topLife;

    public bool slow;
    public int slowTime = 100;
    

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameController").GetComponent<GameManager>();
        objetivo = GameObject.FindGameObjectsWithTag("Bau")[0];
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        topLife = life;
    }
	
	// Update is called once per frame
	void Update () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = new Vector3(objetivo.transform.position.x, objetivo.transform.position.y, objetivo.transform.position.z + 10);


        if (this.slow && this.slowTime >= 0)
        {
            agent.speed = speed / 2;
            this.slowTime--;
        }
        else if(!this.slow) {
            agent.speed = speed;
            this.slowTime = 100;
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bau")
        {
            this.manager.vidas = this.manager.vidas - this.dano;
            this.transform.GetComponentInParent<EnemyLife>().destroy();// Object.Destroy(this.gameObject.transform.parent);
        }
        else 
        {
            Physics.IgnoreCollision(col.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }


    public bool pathIsClear() {
        objetivo = GameObject.FindGameObjectsWithTag("Bau")[0];
        NavMeshPath path = new NavMeshPath();
        GetComponent<NavMeshAgent>().CalculatePath(this.objetivo.transform.position, path);
        Debug.Log(path.status);
        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Object.Destroy(this.gameObject);
            return false;
        }
        return true;
        
    }
}
