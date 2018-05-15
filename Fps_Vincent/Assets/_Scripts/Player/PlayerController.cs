using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 3;
    public float rotateSpeed = 3;
    public float minimumY;
    public float maximumY;

    private CharacterController cc;
    private Animation anim;
    private Vector3 _VecHeroMoving;                        //英雄的移动
    private Vector3 CameraRoation;                         //摄像机旋转
    public float Gravity = 1;                              //英雄的重力
    private Transform camera;
    
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
        camera = Camera.main.transform;

        //光标隐藏与锁定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GlobalManager.heroAction = HeroAction.Standing;
	}
	
	// Update is called once per frame
	void Update () {
        if(GlobalManager.Bloods> 0.01f)
        {
            Moving();
        }
        else
        {
            return;
        }
	}

    void Moving()
    {
        //摄像机（player）的旋转
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        CameraRoation.y += x;                    //沿X轴左右看
        CameraRoation.x -= y;                    //沿Y轴向上看，向下看
        CameraRoation.x = Mathf.Clamp(CameraRoation.x, minimumY, maximumY);
        camera.transform.eulerAngles = CameraRoation;
        this.transform.eulerAngles = new Vector3(0, CameraRoation.y, 0);

        //player的移动
        _VecHeroMoving = Vector3.zero;
        _VecHeroMoving.y -= Gravity;
        if (Input.GetKey(KeyCode.W))
        {
            _VecHeroMoving.z += moveSpeed * Time.deltaTime;
            GlobalManager.heroAction = HeroAction.Walking;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _VecHeroMoving.z -= moveSpeed * Time.deltaTime;
            GlobalManager.heroAction = HeroAction.Walking;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _VecHeroMoving.x -= moveSpeed * Time.deltaTime;
            GlobalManager.heroAction = HeroAction.Walking;
                    }
        else if (Input.GetKey(KeyCode.D))
        {
            _VecHeroMoving.x += moveSpeed * Time.deltaTime;
            GlobalManager.heroAction = HeroAction.Walking;
        }
        else
        {
            GlobalManager.heroAction = HeroAction.Standing;
        }
        //Move() 方法必须使用世界坐标系。
        cc.Move(this.transform.TransformDirection(_VecHeroMoving));

       
    }
}
