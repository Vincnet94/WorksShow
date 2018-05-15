using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    private static PathManager instance;//定义单例
    public static PathManager Instance
    {
        get
        {
            return instance;
        }

    }
    public Transform[] fatPaths;
    public Transform[] malePaths;
    public Transform[] femalePaths;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
