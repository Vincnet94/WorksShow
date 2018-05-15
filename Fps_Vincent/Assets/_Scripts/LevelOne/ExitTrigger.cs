using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour {

    public float gameOverTime = 2f;
    private GameObject player;
    private bool isPlayerExit;
    private float timer;
    private FadeInOut fader;
    public string exitAudio;
    public GameObject succeedPanel;

	// Use this for initialization
	void Start () {
		fader = GameObject.Find("FadeInOut").GetComponent<FadeInOut>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerExit)
        {
            InExitAction();  
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerExit = true;
            AudioManager.Play(exitAudio);
            timer = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player  )
        {
            isPlayerExit = false;
            timer = 0;
        }
    }
    void InExitAction()               //通关后设置
    {
        timer += Time.deltaTime;
       if(timer >= gameOverTime && GlobalManager.getKey)
       {
           fader.EndScene();
           succeedPanel.SetActive(true);
           succeedPanel.GetComponent<TweenPosition>().PlayForward();
           GlobalManager.succeed = true;
           
       }
    }
    //public void Return()
    //{
    //    SceneManager.LoadScene("Menu");
    //}

    
}
