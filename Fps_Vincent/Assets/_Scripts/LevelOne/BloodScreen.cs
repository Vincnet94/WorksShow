using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScreen : MonoBehaviour {

    public static BloodScreen _instance;
    private static BloodScreen Instance
    {
        get
        {
            return _instance;
        }
    }

    private UISprite ui_bloodScreen;
    private TweenAlpha tweenAlpha;

	// Use this for initialization
	void Start () {
        _instance = this;
        ui_bloodScreen = this.GetComponent<UISprite>();
        tweenAlpha = this.GetComponent<TweenAlpha>();
	}
	
	public void Show()
    {
        ui_bloodScreen.enabled = true;
        tweenAlpha.ResetToBeginning();
        tweenAlpha.PlayForward();
    }
}
