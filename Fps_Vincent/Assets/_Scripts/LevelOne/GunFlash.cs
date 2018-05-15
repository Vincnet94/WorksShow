using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlash : MonoBehaviour {

    public Material[] material;
    private int index = 0;
    public float flashTime = 0.05f;
    private float flashTimer = 0;
    private MeshRenderer Mr;
	// Use this for initialization
	void Start () {
        Mr = this.GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Mr.enabled)
        {
            //transform.rotation =Quaternion.Euler( 0,0,Random.Range(0, 30));
            flashTimer += Time.deltaTime;
            if(flashTimer>flashTime)
            {
                Mr.enabled = false;
            }
        }
	}
    public void Flash()
    {
        index++;
        index %= 4;
        Mr.enabled = true;
        Mr.material = material[index];
        flashTimer = 0;
    }
}
