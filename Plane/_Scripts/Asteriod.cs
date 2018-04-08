 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour {

    public float tumble = 10f;
    public float downSpeed = 5f;
    public GameObject explosion;

    public int scoreValue = 10;
    private GameController gameController;



	// Use this for initialization
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * 10;          //在单位球随机值
        Vector3 tMover = new Vector3(0, 0, -1);
        rb.velocity = tMover * downSpeed;

        GameObject go = GameObject.FindWithTag("GameController");
        if (go != null)
        {
            gameController = go.GetComponent<GameController>();
        }
        else
            Debug.Log("找不到");

        if(gameController ==null)
        {
            Debug.Log("找不到脚本");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.z<-16)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter(Collider other)
    {

            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);
        
    }
 
}
