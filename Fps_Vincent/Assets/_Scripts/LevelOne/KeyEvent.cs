using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour {

    public float Speed = 3;

    public AudioClip keyPickUp;
    private GameObject player;
    private bool isPickUpKey;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right * Speed);
	}
    void OnTriggerEnter(Collider other)
    {
        //播放音效
        //弹出提示框
        isPickUpKey = true;
        Destroy(this.gameObject);
    }
}
