using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour {
    public float bulletMoveSpeed = 10;
	// Use this for initialization
	void Start () {
        Vector3 bMover = new Vector3(0, 0, 1);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = bMover * bulletMoveSpeed;
       
        
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z > 15)
        {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    
}
