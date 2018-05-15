using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class femaleEnemyAI : MonoBehaviour
{

    public float EnemyHP = 100;
    private NavMeshAgent agent;
    private GameObject player;
    private Animation anim;
    private UISlider blood;
    private int pathIndex = 0;
    private Rigidbody rb;
    private float timer;

    //public AnimationClip anim_Walking;

    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player");
        blood = GetComponentInChildren<UISlider>();
        rb = GetComponent<Rigidbody>();
        blood.value = 1;
        agent.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        //根据距离进行判断
        if (distance > 20)
        {
            //巡逻
            agent.enabled = false;
            anim.CrossFade("idle");
        }
        else if (distance < 20 && distance > 5)
        {
            //寻路
            agent.enabled = true;
            if (agent && player)
            {
                agent.SetDestination(player.transform.position);
            }
            this.transform.LookAt(player.transform.position);
            anim.CrossFade("Walking");
        }
        else if (distance <= 5)
        {
            agent.enabled = true;
            this.transform.LookAt(player.transform.position);
            anim.CrossFade("attack1");
            //判断当敌人接触到时，玩家才受伤
            GlobalManager.Bloods = GlobalManager.Bloods - (GlobalManager.Bloods * 0.05f);
            BloodScreen._instance.Show();
        }

        //判断死亡
        if (EnemyHP <= 0)
        {
            anim.CrossFade("fallToFace");
            agent.enabled = false;
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
        SceneManager_LevelOne.enemyCount--;
    }

    public void OnShootHurt(int intBlood)
    {
        EnemyHP -= intBlood;
        UI_Damage.instance.Show(intBlood);
        blood.value = EnemyHP / 100;
    }

}


