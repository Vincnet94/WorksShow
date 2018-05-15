using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public UISlider Bloods;
    public UILabel SkillEnemy;
    public GameObject gameOverPanel;
    public UISprite UI_Cursor_Gun;          
    public UISprite UI_Cursor_LongGun;
    public UISprite UI_Cursor_Sinper;
    private float time;                     //检测换枪时间后出现准星
    

	// Use this for initialization
	void Start () {
        InvokeRepeating("ChangeCursor", 0.1f, 0.1f);          //切换准星
	}
	
	// Update is called once per frame
	void Update () {
        Bloods.value = GlobalManager.Bloods;
        SkillEnemy.text = GlobalManager.SkillEnemyNumber.ToString();
        if(GlobalManager.Bloods <=0.1f)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.GetComponent<TweenPosition>().PlayForward();
        }
	}
    void ChangeCursor()
    {
        switch (HeroShootingControl.gunType)
        {
            case GunType.None:
                 UI_Cursor_Gun.enabled = false;
                UI_Cursor_LongGun.enabled = false;
                UI_Cursor_Sinper.enabled = false;
                break;
            case GunType.Gun:
                
                UI_Cursor_Gun.enabled = true;
                UI_Cursor_LongGun.enabled = false;
                UI_Cursor_Sinper.enabled = false;
                break;
            case GunType.LongGun:
                 UI_Cursor_Gun.enabled = false;
                UI_Cursor_LongGun.enabled = true;
                UI_Cursor_Sinper.enabled = false;
                break;
            case GunType.Sniper:
                if(HeroShootingControl.IsSniperCameraOpen)
                {
                    UI_Cursor_Gun.enabled = false;
                    UI_Cursor_LongGun.enabled = false;
                    UI_Cursor_Sinper.enabled = true;
                    
                }
                else
                {
                    UI_Cursor_Gun.enabled = false;
                    UI_Cursor_LongGun.enabled = false;
                    UI_Cursor_Sinper.enabled = false;
                }
               
                break;
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
