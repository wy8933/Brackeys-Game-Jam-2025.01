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
    public AudioClip[] UIClips;
    public AudioClip[] LoopingSFXAssets;
    public AudioClip[] OneShotSFXAssets;
    public Dictionary<string, AudioClip> UIclipDictionary = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> SFXLoopsclipDictionary = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> SFXOneShotsclipDictionary = new Dictionary<string, AudioClip>();

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

        playLoopingSFX("SFX_Clock_Ticking",SFX_Clock);
        playLoopingSFX("SFX_Ambience_Fireplace",SFX_Fireplace);
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



}
