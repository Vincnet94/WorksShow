using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlobalManager : MonoBehaviour {

    public static float Audio_Background = 1;          //背景音量
    public static float Audio_Effect = 1;
    public static float Audio_Gun= 1; 
    public static int SkillEnemyNumber = 0;           //杀敌数量
    public static int GameScore =0;                   //游戏分数
    public static float Bloods = 1000f;                  //英雄血量
    public static bool getKey = false;
    public static bool succeed = false;

    public static HeroPosition heroPosition = HeroPosition.None;
    public static HeroAction heroAction = HeroAction.None;
    
}
