using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public Boundary boundary;              //边界
    public float tilt = 4f;                //飞机旋转

    public float fireRate = 0.5f;
    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire = 0;

    public GameObject playerExplosion;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindWithTag("GameController");
        if (go != null)
        {
            //gameController = gameObject.GetComponent<GameController>();
            gameController = go.GetComponent<GameController>();        //不能直接得到脚本对象，而需要通过GameObject.FindWithTag得到GameController
        }
        else
            Debug.Log("找不到");

        if (gameController == null)
        {
            Debug.Log("找不到脚本");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1")&& Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
        }
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal,0,moveVertical);
         //GetComponent<Rigidbody>().velocity = movement*speed;            //哈哈

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.rotation = Quaternion.Euler(0,0,rb.velocity.x*-tilt);
        if(rb != null)
        {
            rb.velocity = movement * speed;
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0, Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax));

        }

    }

    void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);
        Instantiate(playerExplosion, transform.position, transform.rotation);
        gameController.GameOver();
    }
}
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
