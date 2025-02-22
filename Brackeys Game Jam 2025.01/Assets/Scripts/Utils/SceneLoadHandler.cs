using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHandler : MonoBehaviour
{
    public void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If main game scene is loaded, play correct looping sounds
        if (scene.name == "WeijieTestScene")
        {
            SoundManager.Instance.playLoopingSFX("SFX_Clock_Ticking",SoundManager.Instance.SFX_Clock);
            SoundManager.Instance.playLoopingSFX("SFX_Ambience_Fireplace",SoundManager.Instance.SFX_Fireplace);
            SoundManager.Instance.playLoopingMusic("BGM",SoundManager.Instance.BGM);
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense);
        }
        else if (scene.name == "MainMenu") //play correct sounds if menu is loaded
        {
            SoundManager.Instance.playLoopingMusic("BGM",SoundManager.Instance.BGM);//make this menu music
        }
        Debug.Log("New scene loaded: " + scene.name);

        // You can add your custom logic here
    }
}