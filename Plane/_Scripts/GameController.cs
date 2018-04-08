using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject asteriod;
    public Vector3 spawnValues;
    private Vector3 spawnPosition=Vector3.zero;

    public int asteriodCount  = 10;
    public float spawnWait = 2;               //生成单个行星延迟时间
    public float startWait = 1;               //开始生成行星的时间

    public float waveWait = 5f;               //下一波等待时间

    public Text scoreText;                    //更新
    private int score;                        //当前分值

    public Text gameOverText;
    private bool gameOver;

    public Text restartText;
    private bool restart;

	// Use this for initialization
	void Start () {
        score = 0;
        UpdateScore();
        gameOverText.text = "";             //字体消失
        gameOver = false;
        restartText.text = "";
        restart =false;

        StartCoroutine( SpawnWaves());

	}
	
	// Update is called once per frame
	void Update () {
		if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(0);
            }
        }
	}
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)                                                    //无限
        {
           
                for (int i = 0; i < asteriodCount; i++)                         //第一波行星
                {
                    if (!gameOver)
                    {
                        spawnPosition.x = Random.Range(-spawnValues.x, spawnValues.x);
                        spawnPosition.z = spawnValues.z;

                        Instantiate(asteriod, spawnPosition, Quaternion.identity);
                        yield return new WaitForSeconds(spawnWait);
                    }
                    else
                    {
                        restartText.text = "按【R】键重新开始";
                        restart = true;
                        break;
                    }
                        
                }
                yield return new WaitForSeconds(waveWait);                    //准备第二波行星

            
        }
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "得分： " + score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "游戏结束";
    }
}

