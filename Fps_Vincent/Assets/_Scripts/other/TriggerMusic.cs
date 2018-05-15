using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour {

    public AudioClip road_BackGround;
	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            PlaySound(road_BackGround);
            GetComponent<AudioSource>().loop = true;
        }
    }
    void PlaySound(AudioClip ac)
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().PlayOneShot(ac);
            GetComponent<AudioSource>().loop = true;
        }
        else
        {
            this.gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().PlayOneShot(ac);
            
        }

    }
}
