using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour {

    public float deadZone = 5f;
    private Transform player;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private HashIDs hash;
    private AnimatorSetup animSetup;
    private Animator anim;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemySight = this.GetComponent<EnemySight>();
        nav = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("Scene").GetComponent<HashIDs>();
        animSetup = new AnimatorSetup(anim,hash);
        nav.updateRotation = false;
        anim.SetLayerWeight(1,1f);
        anim.SetLayerWeight(2,1f);
        deadZone *= Mathf.Deg2Rad;
	}

    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }
	void NavAnimSetup()
    {
        float speed;
        float angle;
        if(enemySight.playerInSight)
        {
            speed = 0;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;        //??
            angle = FindAngle(transform.forward,nav.desiredVelocity,transform.up);
            if(Mathf.Abs(angle)<deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0;
            }
        }
        animSetup.Setup(speed,angle);
    }

    private float FindAngle(Vector3 fromVector,Vector3 toVector,Vector3 upVector)
    {
        if(toVector == Vector3.zero)
        {
            return 0f;
        }
        float angle = Vector3.Angle(fromVector, toVector);                   //为得到有方向的角度
        Vector3 normal = Vector3.Cross(fromVector,toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal,upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }
	// Update is called once per frame
	void Update () {
        NavAnimSetup();
	}
}
