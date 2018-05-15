using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMusic : MonoBehaviour {
    public string AudioName;
    public string AudioEffectName;
    public UISlider UI_Background;
    public UISlider UI_AudioEffect;

    public BoxCollider UI_Main;

    void Start()
    {
        AudioManager.PlayBackground(AudioName);
    }

    public void Volumns()
    {
        AudioManager.Play(AudioEffectName);
        UI_Main.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void BackgroundVolumns()
    {
        AudioManager.Change_BGVolumns(UI_Background.value);
    }
    public void AudioVolumns()
    {
        AudioManager.Change_AEVolumns(UI_AudioEffect.value);
    }

    public void EnableSlider()
    {
        UI_Background.gameObject.GetComponent<UISlider>().enabled = true;
        UI_AudioEffect.gameObject.GetComponent<UISlider>().enabled = true;
        UI_Main.gameObject.GetComponent<BoxCollider>().enabled = false;
        UI_Background.value = GlobalManager.Audio_Background;
        UI_AudioEffect.value = GlobalManager.Audio_Effect;
            
    }

}
