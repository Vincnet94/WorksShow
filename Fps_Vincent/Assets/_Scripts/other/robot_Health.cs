using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_Health : MonoBehaviour {

    public float hp = 100;
    private Animator anim;
    private HashIDs hash;
    private bool isDead = false;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("Scene").GetComponent<HashIDs>();
	}

   public  void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp<=0 && !isDead)
        {
            isDead = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<EnemyAnimation>().enabled = false;
            GetComponent<robot_EnemyAI>().enabled = false;
            GetComponent<EnemySight>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponentInChildren<Light>().enabled = false;
            GetComponentInChildren<LineRenderer>().enabled = false;

            anim.SetBool(hash.playerInSightBool, false);
            anim.SetBool(hash.deadBool,true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
