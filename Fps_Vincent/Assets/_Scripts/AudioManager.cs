using System.Collections;
using System.Collections.Generic;           //泛型集合命名控件
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] AudioClipArray;          //音频剪辑数组
    private static Dictionary<string, AudioClip> DicAudioClipLib; //音频库

    private static AudioSource[] AudioSourceArray;       //音频源数组
    private static AudioSource BackgroundAS;           //背景音乐
    private static AudioSource AudioEffect;            //背景音效
    private static AudioSource GunEffect;            //枪音效
    public Vector2 pitchFire = new Vector2(1f, 1.1f);         //步枪射击频率

	// Use this for initialization
    void Awake()
    {
        DicAudioClipLib = new Dictionary<string, AudioClip>();
        foreach (AudioClip audioClip in AudioClipArray)            //音频库加载
        {
            DicAudioClipLib.Add(audioClip.name, audioClip);
        }

        AudioSourceArray = this.GetComponents<AudioSource>();
        BackgroundAS = AudioSourceArray[0];
        AudioEffect = AudioSourceArray[1];
        GunEffect = this.GetComponent<AudioSource>();

        //提取存储中的音量数值
        if (PlayerPrefs.GetFloat("Audio_Background") !=null)
        {
            GlobalManager.Audio_Background = PlayerPrefs.GetFloat("Audio_Background");
        }
        if (PlayerPrefs.GetFloat("Audio_Effect") != null) 
        {
            GlobalManager.Audio_Effect = PlayerPrefs.GetFloat("Audio_Effect");
        }
    }

    public static void PlayBackground(AudioClip audioClip)    //播放背景音乐
    {
        if (BackgroundAS.clip == audioClip)            //防止背景音乐重复播放
        {
            return;
        }
        BackgroundAS.volume = GlobalManager.Audio_Background;
        if(audioClip)
        {
            BackgroundAS.clip = audioClip;
            BackgroundAS.Play();
        }
        else
        {
            Debug.LogWarning("音频为空");
        }
    }

    public static void PlayBackground(string strAudioName)       //调用 PlayBackground（）
    {
        if(!string.IsNullOrEmpty(strAudioName))
        {
            PlayBackground(DicAudioClipLib[strAudioName]);  //字典键值索引AudioClip
        }
        else
        {
            Debug.LogWarning("音频名为空");
        }
    }
	
    private static void Play(AudioClip audioClip)            //播放音效
    {
        AudioEffect.volume = GlobalManager.Audio_Effect;
        if(audioClip)
        {
            AudioEffect.clip = audioClip;
            AudioEffect.Play();
        }
        else
        {
            Debug.LogWarning("音效为空");
        }
    }

    public static void Play(string strAudioEffectName)
    {
        if(!string.IsNullOrEmpty(strAudioEffectName))
        {
            Play(DicAudioClipLib[strAudioEffectName]);
        }
        else
        {
            Debug.LogWarning("音效为空");
        }
    }

    public static void GunBackground(AudioClip audioClip)    //播放手动枪音效
    {

        GunEffect.volume = GlobalManager.Audio_Gun;
        if (audioClip)
        {
            GunEffect.clip = audioClip;
            GunEffect.Play();
        }
        else
        {
            Debug.LogWarning("音频为空");
        }
    }

    public static void AutoBackground(AudioClip audioClip)    //播放半自动枪音效
    {

        GunEffect.volume = GlobalManager.Audio_Gun;
        if (audioClip)
        {
            GunEffect.clip = audioClip;
            GunEffect.pitch = Random.Range(1f, 1.1f) * Time.timeScale;
            GunEffect.Play();
        }
        else
        {
            Debug.LogWarning("音频为空");
        }
    }


    public static void Change_BGVolumns(float volumns)
    {
        BackgroundAS.volume = volumns;
        GlobalManager.Audio_Background = volumns;
        PlayerPrefs.SetFloat("Audio_Background",volumns);         //存放音效记录
    }
    public static void Change_AEVolumns(float volumns)
    {
        AudioEffect.volume = volumns;
        GlobalManager.Audio_Effect = volumns;
        PlayerPrefs.SetFloat("Audio_Effect", volumns);           //存放音效记录
    }
}
