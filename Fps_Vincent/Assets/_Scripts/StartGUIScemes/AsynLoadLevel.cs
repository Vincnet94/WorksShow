using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsynLoadLevel : MonoBehaviour {

    public UISlider UI_Progress;
    public string NextScenesName;
    private AsyncOperation AsynLoad;            //异步加载

    void Awake()
    {
        UI_Progress.GetComponent<UISlider>().value = 0;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(EnterNextScenes());
	}
	
	IEnumerator EnterNextScenes()
    {
        float i = 0;
        AsynLoad = SceneManager.LoadSceneAsync(NextScenesName);
        AsynLoad.allowSceneActivation = false;
        while(i<=100)
        {
            i++;
            UI_Progress.GetComponent<UISlider>().value = i/100;
            yield return new WaitForEndOfFrame();
        }
        AsynLoad.allowSceneActivation = true;
        
    }
}
