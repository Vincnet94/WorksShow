using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 playerPosition;
    public Vector3 resetPosition = Vector3.zero;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private GameObject player;
    private HashIDs hash;

	// Use this for initialization
	void Start () {
		nav = this.GetComponent<NavMeshAgent>();
        col = GetComponentInChildren<SphereCollider>();
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        hash = GameObject.FindGameObjectWithTag("Scene").GetComponent<HashIDs>();
	}

    void Update()
    {
        if(GlobalManager.Bloods>0)
        {
            anim.SetBool(hash.playerInSightBool, playerInSight);
        }
        else
            anim.SetBool(hash.playerInSightBool, false);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject ==player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;            //玩家位置 - 敌人位置
            float angle = Vector3.Angle(direction, transform.forward);
            if(angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + transform.up,direction.normalized,out hit,col.radius))
                {
                    if(hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        playerPosition = player.transform.position;
                    }
                }
            }
            if(GlobalManager.heroAction ==HeroAction.Walking)             //侦听玩家声音
            {
                ListenPlayer();
            }
        }
    }
	
	

    void ListenPlayer()
    {
        if(Vector3.Distance(player.transform.position,transform.position)<=col.radius)
        {
            playerPosition = player.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;
        }
    }
}
