using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum GunType 
    { 
      None, 
      Gun, 
      LongGun, 
      Sniper 
    }
public class HeroShootingControl : MonoBehaviour {

    public Transform buttlePoint;                           //子弹生成位置
    private Transform camera;                               //摄像机位置
  
    public string EnemyShootName;
    public static GunType gunType ;           //枪支类型
    private int ShootingLength = 5000000;                   //射线距离
    public static bool IsSniperCameraOpen = false;          //是否开启狙击镜头
    private float timer;
    

    public AudioClip gunClip;
    public AudioClip sniperClip;
    public AudioClip longGunClip;   

    private bool isGun =true;
    private bool isSniper = true;
    private bool isLongGun = true;
 
    public GunFlash gunFlash;
    public GunFlash longgunFlash;
    public GunFlash snipergunFlash;
    private bool isFire;
    private float flashTimer;

    private Animation anim_Gun;
    private Animation anim_LongGun;
    private Animation anim_Sniper;

    private float FlashTimer;
    public int bulletCount_Gun = 9;                          //手枪子弹数
    public int allBulletCount_Gun = 72;                      //手枪子弹总数
    public int bulletCount_LongGun = 30;                     //步枪枪子弹数
    public int allBulletCount_LongGun = 150;                  //步枪枪子弹总数
    public int bulletCount_Sniper = 5;                       //狙击枪枪子弹数
    public int allBulletCount_Sniper = 25;                   //狙击枪子弹总数

    private int currentBullet_Gun;                           //当前手枪子弹数量
    private int currentAllBullet_Gun;                        //当前手枪可装载子弹数量

    private int currentBullet_LongGun;                       //当前步枪子弹数量
    private int currentAllBullet_LongGun;                    //当前步枪可装载子弹数量

    private int currentBullet_Sniper;                        //当前狙击枪子弹数量
    private int currentAllBullet_Sniper;                     //当前狙击枪可装载子弹数量
    public UILabel bulletText;
    public float reloadTime = 1.5f;

    public GameObject explosion;                             //特效
	// Use this for initialization
	void Start () {
        camera = Camera.main.transform;                       //枪口位置
        anim_Gun = camera.Find("M9_anim").GetComponent<Animation>();
        anim_LongGun = camera.Find("AK47_anim").GetComponent<Animation>();
        anim_Sniper = camera.Find("MK11_anim").GetComponent<Animation>();
        currentBullet_Gun = bulletCount_Gun;
        currentAllBullet_Gun = allBulletCount_Gun;

        currentBullet_LongGun = bulletCount_LongGun;
        currentAllBullet_LongGun = allBulletCount_LongGun;

        currentBullet_Sniper = bulletCount_Sniper;
        currentAllBullet_Sniper = allBulletCount_Sniper;
        GlobalManager.SkillEnemyNumber = 0;
        gunType = GunType.None;
	}
	
	// Update is called once per frame
	void Update () {
        if(GlobalManager.getKey &&GlobalManager.succeed)
        {
            return;
        }
        if (GlobalManager.Bloods > 0.01f)
        {
            GetKeyboardInputKey();
            ChangeGunType();             //切换到冷兵器不能射击
            ShootType();           
        }
	}

    void GetKeyboardInputKey()               //键盘按键换枪
    {
        //改变射线的距离
        //改变音效
        timer += Time.deltaTime;
         if(Input.GetKeyDown(KeyCode.Alpha1))         //手枪
         {
             if (timer >= 0 &&isGun)
             {
                 gunType = GunType.Gun;
                 ShootingLength = 50;
                 //Animation anim = camera.Find("M9_anim").GetComponent<Animation>();
                 anim_Gun.CrossFade("M9_ChangeGun");
                 timer = 0 - anim_Gun["M9_ChangeGun"].length;
                 isGun = false;
                 isLongGun = true;
                 isSniper = true;
             }
             bulletText.text = currentBullet_Gun + "/" + currentAllBullet_Gun; 
         }
         else if(Input.GetKeyDown(KeyCode.Alpha2))         //步枪
         {
             if (timer >= 0 &&isLongGun)
             {
                 gunType = GunType.LongGun;
                 ShootingLength = 200;
                 anim_LongGun.CrossFade("AK47_ChangeGun");          
                 timer = 0 - anim_LongGun["AK47_ChangeGun"].length;
                 isGun =  true;
                 isLongGun =false;
                 isSniper = true;
             }
             bulletText.text = currentBullet_LongGun + "/" + currentAllBullet_LongGun; 
         }
         else if (Input.GetKeyDown(KeyCode.Alpha3))         //狙击枪
         {
             if (timer >= 0 &&isSniper)
             {
                 gunType = GunType.Sniper;
                 ShootingLength = 800;
                 anim_Sniper.CrossFade("MK11_ChangeGun");
                 timer = 0 - anim_Sniper["MK11_ChangeGun"].length;
                 isSniper = false;
                 isGun = true;
                 isLongGun = true;
             }
             bulletText.text = currentBullet_Sniper + "/" + currentAllBullet_Sniper; 
         }
         else if(Input.GetKeyDown(KeyCode.Alpha4))
         {
             //冷兵器
             gunType = GunType.None;
             isSniper = true;
             isGun = true;
             isLongGun = true;
             ShootingLength = 60;
             bulletText.text = " ";
         }
    }

    void ChangeGunType()
     {
        //所有枪不渲染
         GameObject[] GunArray = GameObject.FindGameObjectsWithTag("Gun");
        foreach(GameObject item in GunArray)
        {
            item.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        //特定枪支渲
        switch(gunType)
        {
            case GunType.None:  
                break;

            case GunType.Gun: camera.Find("M9_anim/hand").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                camera.Find("M9_anim/M9_pistol").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                break;

            case GunType.LongGun: camera.Find("AK47_anim/hand").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                camera.Find("AK47_anim/ak47").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                break;

            case GunType.Sniper: camera.Find("MK11_anim/hand").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                camera.Find("MK11_anim/MK-12").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                break;
        }
        //特定枪支镜头远近的处理
        switch(gunType)
        {
            case GunType.None:
                camera.GetComponent<Camera>().fieldOfView = 60;
                break;
            case GunType.Gun: camera.GetComponent<Camera>().fieldOfView = 60;               
                break;
            case GunType.LongGun: camera.GetComponent<Camera>().fieldOfView = 50;               
                break;
            case GunType.Sniper:               
                break;
        }
     }

    void ShootType()                                  
    {
        flashTimer += Time.deltaTime;               
        if (gunType == GunType.Sniper)
        {
            if (flashTimer >= 0)                        //阻击射击间隔
            {
            if (Input.GetMouseButtonDown(1))
            {
                IsSniperCameraOpen = true;
                camera.GetComponent<Camera>().fieldOfView = 10;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (currentBullet_Sniper > 0)
                {
                camera.GetComponent<Camera>().fieldOfView = 60;
                AudioManager.GunBackground(sniperClip);
                snipergunFlash.Flash();
                Shooting();
                currentBullet_Sniper--;
                }
                bulletText.text = currentBullet_Sniper + "/" + currentAllBullet_Sniper;
                if (currentBullet_Sniper == 0 && currentAllBullet_Sniper!=0)
                {
                    anim_Sniper.CrossFade("MK11_ChangeButtle");
                    StartCoroutine(ReloadSniper());
                }
                IsSniperCameraOpen = false;
                flashTimer = -2;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentAllBullet_Sniper > 0)
                {
                    anim_Gun.CrossFade("MK11_ChangeButtle");
                    StartCoroutine(ReloadSniper());
                    currentBullet_Sniper--;
                    bulletText.text = currentBullet_Sniper + "/" + currentAllBullet_Sniper;
                }
            }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Shooting();
            }
            switch (gunType)
            {
                        case GunType.None:
                            break;
                        case GunType.Gun:
                            if (Input.GetMouseButtonDown(0))
                            {
                                isFire = true;
                                if (isFire)
                                {
                                    if (currentBullet_Gun > 0)
                                    {
                                        AudioManager.GunBackground(gunClip);
                                        gunFlash.Flash();
                                        Shooting();
                                        currentBullet_Gun--;
                                    }
                                    bulletText.text = currentBullet_Gun + "/" + currentAllBullet_Gun;
                                    if (currentBullet_Gun == 0 &&  currentAllBullet_Gun!=0)
                                    {
                                        anim_Gun.CrossFade("M9_ChangeButtle");
                                        StartCoroutine(ReloadGun());
                                    }
                                }
                            }
                            if (Input.GetMouseButtonUp(0))
                            {
                                isFire = false;
                            }
                            if (Input.GetKeyDown(KeyCode.R))
                            {
                                if (currentAllBullet_Gun > 0)
                                {
                                    anim_Gun.CrossFade("M9_ChangeButtle");
                                    StartCoroutine(ReloadGun());
                                    currentBullet_Gun--;
                                    bulletText.text = currentBullet_Gun + "/" + currentAllBullet_Gun;
                                }
                            }
                            break;

                        case GunType.LongGun:
                            if (Input.GetMouseButton(0))
                            {
                                if (currentBullet_LongGun > 0)
                                {
                                    AudioManager.GunBackground(longGunClip);
                                    longgunFlash.Flash();
                                    Shooting();
                                    currentBullet_LongGun--;
                                }
                                bulletText.text = currentBullet_LongGun + "/" + currentAllBullet_LongGun;
                                if (currentBullet_LongGun == 0 &&currentAllBullet_LongGun !=0)
                                {
                                    anim_LongGun.CrossFade("AK47_ChangeButtle");
                                    StartCoroutine(ReloadLongGun());
                                }
                            }
                            if (Input.GetKeyDown(KeyCode.R))
                            {
                                if (currentAllBullet_LongGun > 0)
                                {
                                    anim_LongGun.CrossFade("AK47_ChangeButtle");
                                    StartCoroutine(ReloadLongGun());
                                    currentBullet_LongGun--;
                                    bulletText.text = currentBullet_LongGun + "/" + currentAllBullet_LongGun;
                                }
                            }
                            break;
                        default:
                            break;
                    }
            }
        }


    void Shooting()
    {
        RaycastHit hit;
        bool boolResult = Physics.Raycast(buttlePoint.position, camera.TransformDirection(Vector3.forward), out hit, ShootingLength);
        if (boolResult)
        {
            if (hit.collider.gameObject.tag.Equals("Enemy"))
            {
                switch (gunType)
                {
                    case GunType.None:
                        break;
                    case GunType.Gun:
                        if (hit.collider.gameObject.name =="robot")
                        {
                            hit.collider.gameObject.GetComponent<EnemyAIShoot>().OnShootHurt(1);
                            print("1111");
                        }
                        else
                        hit.collider.gameObject.GetComponent<EnemyAI>().OnShootHurt(8);
                        //击中声音
                        GameObject go = Instantiate(explosion, hit.point, Quaternion.identity);
                        Destroy(go, 3);
                        break;
                    case GunType.LongGun:
                        hit.collider.gameObject.GetComponent<EnemyAI>().OnShootHurt(15);
                        break;
                    case GunType.Sniper:
                        hit.collider.gameObject.GetComponent<EnemyAI>().OnShootHurt(35);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(reloadTime);
        if(currentAllBullet_Gun >= bulletCount_Gun - currentBullet_Gun )
        {
            currentAllBullet_Gun -= (bulletCount_Gun - currentBullet_Gun);
            currentBullet_Gun = bulletCount_Gun;
        }
        else
        {
            currentBullet_Gun += currentAllBullet_Gun;
            currentAllBullet_Gun = 0;
        }
        bulletText.text = currentBullet_Gun + "/" + currentAllBullet_Gun;
    }

    IEnumerator ReloadLongGun()
    {
        yield return new WaitForSeconds(reloadTime);
        if (currentAllBullet_LongGun >= bulletCount_Gun - currentBullet_LongGun)
        {
            currentAllBullet_LongGun -= (bulletCount_LongGun - currentBullet_LongGun);
            currentBullet_LongGun = bulletCount_LongGun;
        }
        else
        {
            currentBullet_LongGun += currentAllBullet_LongGun;
            currentAllBullet_LongGun = 0;
        }
        bulletText.text = currentBullet_LongGun + "/" + currentAllBullet_LongGun;
    }

    IEnumerator ReloadSniper()
    {
        yield return new WaitForSeconds(reloadTime);
        if (currentAllBullet_Sniper >= bulletCount_Sniper - currentBullet_Sniper)
        {
            currentAllBullet_Sniper -= (bulletCount_Sniper - currentBullet_Sniper);
            currentBullet_Sniper = bulletCount_Sniper;
        }
        else
        {
            currentBullet_Sniper += currentAllBullet_Sniper;
            currentAllBullet_Sniper = 0;
        }
        bulletText.text = currentBullet_Sniper + "/" + currentAllBullet_Sniper;
    }
}
