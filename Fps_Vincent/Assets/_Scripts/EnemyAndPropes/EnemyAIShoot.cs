using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIShoot : MonoBehaviour {

    public float enemyRobotHP = 100;
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject player;
    private Animation anim;
    private UISlider blood;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = this.GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player");
        blood = GetComponentInChildren<UISlider>();
        blood.value = 1;
	}

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        //根据距离进行判断
        if (distance > 25)
        {
            //巡逻
            anim.CrossFade("run");
            //播放巡逻动画
        }
        else if (distance < 25 && distance > 12)
        {
            //寻路
            if (agent && player)
            {
                agent.SetDestination(player.transform.position);
            }
            this.transform.LookAt(player.transform.position);
            //播放行走动画
            anim.CrossFade("run");
        }
        else if (distance <= 12)
        {
            this.transform.LookAt(player.transform.position);
            anim.CrossFade("shoot");
            GlobalManager.Bloods = GlobalManager.Bloods - (GlobalManager.Bloods * 0.005f);
            BloodScreen._instance.Show();
        }


        //判断死亡
        if (enemyRobotHP <= 0)
        {
            anim.CrossFade("hit");
            OnDeath();
        }
    }

    void OnDeath()
    {
        //增加杀敌数量
        //增加分数
        //销毁
        GlobalManager.SkillEnemyNumber++;
        GlobalManager.GameScore++;
        Destroy(this.gameObject);
    }

    public void OnShootHurt(int intBlood)
    {
        enemyRobotHP -= intBlood;
        UI_Damage.instance.Show(intBlood);
        blood.value = enemyRobotHP / 100;
    }
}
