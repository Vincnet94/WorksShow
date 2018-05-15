using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunMusic : MonoBehaviour {

	// Use this for initialization
    void PlaySound(AudioClip ac)
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().PlayOneShot(ac);
        }
        else
        {
            this.gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().PlayOneShot(ac);
        }

    }
}
