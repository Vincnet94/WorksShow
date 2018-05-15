using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager_LevelOne : MonoBehaviour {

    public GameObject Enemy;
    public Transform birthSpawn;
   // public GameObject[] EnemyArray;             //可设置敌人数组，随机生成
    public Transform player;
    public float number = 5;
    public static float enemyCount = 0;
    private float time = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(GreateEnemyPerfab());
           
	}
	
	// Update is called once per frame
	void Update () {
       
        
	}

    IEnumerator GreateEnemyPerfab()           //按地点生成敌人，或按索引
    {
         //float birthDistance = Vector3.Distance(player.position,birthSpawn.position);
        //判断主角与各个出生点距离，从而生成某个出生点的敌人
        //if (birthDistance < 5)
        //{
        for (int i = 0; i < number; i++)                 //实现敌人出现的波数
        {
            //EnemySpawn(Enemy, birthSpawn.transform.position, 3);
            for (int j = 0; j <= 5; j++)
            {//每一波实例化的个数
                //Instantiate(要实例化的物体，在哪个位置实例化，是否旋转)实例化方法
                Instantiate(Enemy, birthSpawn.position, Quaternion.identity);
                enemyCount++;
                yield return new WaitForSeconds(1);//延时1秒
            }
            while(enemyCount >0)
            {
                yield return 0;
            }
           
        }
       // }
    }

    void EnemySpawn(GameObject enemy,Vector3 birthPoint,int number)
    {
        
            for (int i = 1; i <= number; i++)
            {
                GameObject go = GameObject.Instantiate(enemy);
                go.transform.position = new Vector3(birthPoint.x + Random.Range(0, 10), birthPoint.y, birthPoint.z + Random.Range(0, i));
                go.SetActive(true);
            }       
    }
}
