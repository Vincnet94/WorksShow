using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterNextLevel : MonoBehaviour {
    
    public string NextLevelName;
   
    public void OnClick()
    {
        SceneManager.LoadScene(NextLevelName);
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
