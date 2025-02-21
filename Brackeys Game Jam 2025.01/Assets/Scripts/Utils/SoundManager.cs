using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public float SFXMult;
    public float MusicMult;
    public AudioSource UI;
    public AudioSource SFX_Looping;
    public AudioSource SFX_OneShots;
    public AudioSource BGM;
    public AudioClip[] UIClips;
    public Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

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
            clipDictionary[clip.name] = clip;
        }
    }

    public void Play_UI(string clipName)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            UI.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"Clip with ID '{clipName}' not found!");
        }

    }


}
