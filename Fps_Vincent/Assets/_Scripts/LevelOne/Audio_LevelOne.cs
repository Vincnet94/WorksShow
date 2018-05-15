using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_LevelOne : MonoBehaviour {

    public AudioClip audio_1;          //
    public AudioClip audio_2;
    //public AudioClip audio_4;

    public string PlayerAudio;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Check_BGAudio",1F,5F);                                 //背景音乐播放
        InvokeRepeating("Check_PlayerAudio",1,0.5f);                            //玩家音效播放
	}
	
	 private void Check_BGAudio()
    {
        switch (GlobalManager.heroPosition)
        {
            case HeroPosition.None:
                break;
            case HeroPosition.Road:
                AudioManager.PlayBackground(audio_1);               //在路上
                break;
            case HeroPosition.Corner:
                AudioManager.PlayBackground(audio_2);
                break;
            default:
                break;
        }
    }
     private void Check_PlayerAudio()
     {
         switch(GlobalManager.heroAction)
         {
             case HeroAction.None:
                 break;
             case HeroAction.Standing:
                 break;
             case HeroAction.Walking:
                 if (GlobalManager.Bloods > 0.05f)
                 {
                     AudioManager.Play(PlayerAudio);
                 }
                 break;
         }
     }
}
