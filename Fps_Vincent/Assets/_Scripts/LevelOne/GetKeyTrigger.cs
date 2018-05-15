using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyTrigger : MonoBehaviour {

    private GameObject player;
    public string getAudio;
    public GameObject getKeyPanel;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}

   void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player)
        {
            GlobalManager.getKey = true;
            AudioManager.Play(getAudio);
            getKeyPanel.SetActive(true);
            getKeyPanel.GetComponent<TweenPosition>().PlayForward();
            
        }    
    }
    void OnTriggerExit(Collider other)
   {
       getKeyPanel.SetActive(false);
       Destroy(this.gameObject);
   }
}
