using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class robot_EnemyAI : MonoBehaviour {

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoint;

    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

	// Use this for initialization
	void Start () {
        enemySight = this.GetComponent<EnemySight>();
        nav = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
	}
	
	// Update is called once per frame
	void Update () {
		if(enemySight.playerInSight && GlobalManager.Bloods>0)
        {
            Shooting();
        }
        else if (enemySight.playerPosition != enemySight.resetPosition && GlobalManager.Bloods > 0)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
	}
    void Shooting()
    {
        nav.SetDestination(transform.position);
    }

    void Chasing()
    {
        Vector3 sightingDeltaPos = enemySight.playerPosition - transform.position;
        if(sightingDeltaPos.sqrMagnitude >4)
        {
            nav.destination = enemySight.playerPosition;
        }
        nav.speed = chaseSpeed;

        if(nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if(chaseTimer >= chaseWaitTime)
            {
                enemySight.playerPosition = enemySight.resetPosition;
                chaseTimer = 0;
            }
        }
        else
        {
            chaseTimer = 0;
        }
    }

    void Patrolling()
    {
        nav.speed = patrolSpeed;
        if(nav.destination == enemySight.resetPosition ||nav.remainingDistance<nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if(patrolTimer>=patrolWaitTime)
            {
                if(wayPointIndex == patrolWayPoint.Length -1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                patrolTimer = 0;
            }
            else
            {
                patrolTimer = 0;
            }
            nav.destination = patrolWayPoint[wayPointIndex].position;
        }
    }
}
