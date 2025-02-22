using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public float SFXMult;
    public float MusicMult;
    public AudioSource UI;
    public AudioSource SFX_Fireplace;
    public AudioSource SFX_Clock;
    public AudioSource SFX_OneShots;
    public AudioSource BGM;
    public AudioSource BGM_Intense;
    public AudioLowPassFilter BGM_Intense_LPF;
    public AudioClip[] UIClips;
    public AudioClip[] LoopingSFXAssets;
    public AudioClip[] OneShotSFXAssets;
    public AudioClip[] MusicAssets;
    public Dictionary<string, AudioClip> UIclipDictionary = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> SFXLoopsclipDictionary = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> SFXOneShotsclipDictionary = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> MusicclipDictionary = new Dictionary<string, AudioClip>();

    public void Awake()
    {
        if (Instance == null)
        {
            UI.volume = SFXMult = 1.0f;
            MusicMult = 1.0f;
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }

        foreach (var clip in UIClips)
        {
            UIclipDictionary[clip.name] = clip;
        }
       
        foreach (var clip in LoopingSFXAssets)
        {
            SFXLoopsclipDictionary[clip.name] = clip;
        }
        
        foreach (var clip in OneShotSFXAssets)
        {
            SFXOneShotsclipDictionary[clip.name] = clip;
        }
        foreach (var clip in MusicAssets)
        {
            MusicclipDictionary[clip.name] = clip;
        }

        BGM_Intense_LPF = BGM_Intense.GetComponent<AudioLowPassFilter>();
         
    }
    public void Start()
    {
    }

    public void Play_OneShot(string clipName, Dictionary<string,AudioClip> clipDictionary, AudioSource audioSource)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"Clip with ID '{clipName}' not found!");
        }

    }
    public void Play_UI(string clipName)
    {
        if (UIclipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            UI.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"Clip with ID '{clipName}' not found!");
        }

    }

    public void playLoopingSFX(string clipName, AudioSource audioSource)
    {
        if (SFXLoopsclipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"Clip with ID '{clipName}' not found!");
        }
    }
    public void playLoopingMusic(string clipName, AudioSource audioSource, float volume)
    {
        if (MusicclipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"Clip with ID '{clipName}' not found!");
        }

    }

    public void FadeAudio(AudioSource audioSource, float fadeOutTime)
    {
        StartCoroutine(FadeAudioCoroutine(audioSource, fadeOutTime));
    }

    public IEnumerator FadeAudioCoroutine(AudioSource audioSource, float fadeOutTime)
    {
        float startVolume = audioSource.volume;
        while( audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }
    }

    public void LerpLPFCutoff(AudioLowPassFilter LPF,float endFreq ,float time)
    {
        LerpLPFCutoffCoroutine(LPF,endFreq,time);
    }

    public IEnumerator LerpLPFCutoffCoroutine(AudioLowPassFilter LPF, float end, float time)
    {
        float lerpTime = time/60;
        float start = LPF.cutoffFrequency;

        if (start > end)
        {
            while (start > end)
            {
                LPF.cutoffFrequency -= end * Time.deltaTime/time;
                yield return null;
            }

        }
        else if (end > start)
        {
            while (end > start)
            {
                LPF.cutoffFrequency += start * Time.deltaTime/time;
            }
        }
    }

}
