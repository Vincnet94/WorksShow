using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHeroPosition : MonoBehaviour {

    public Transform playerPosition;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Position",1,5);
	}
	
	// Update is called once per frame
	void Position()
    {
        if(playerPosition.transform.position.x>=-38 &&playerPosition.transform.position.x<=60 &&playerPosition.transform.position.z<-33 )
        {
            GlobalManager.heroPosition = HeroPosition.Road;
            print("在路上");
        }
       else if (playerPosition.transform.position.x <= 35 && playerPosition.transform.position.x >=-20 &&playerPosition.transform.position.z>=70)
       {
           GlobalManager.heroPosition = HeroPosition.Corner;
           print("在角落");
       }
        else
        {
            GlobalManager.heroPosition = HeroPosition.None;
            //print("播放默认音乐");
        }
    }
}
