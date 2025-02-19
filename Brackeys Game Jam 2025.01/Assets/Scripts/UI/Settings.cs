using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider SFXSlider;
    public Slider MusicSlider;
    private bool canPlaySound = true;
    private float soundCoolDownTime = 0.095f;

    public void Start()
    {
        SFXSlider.value = SoundManager.Instance.SFXMult;
        MusicSlider.value = SoundManager.Instance.MusicMult;
    }

    /// <summary>
    /// Change the SFX value
    /// </summary>
    public void SFXOnValueChange()
    {
        // SoundManager.Instance.SFXMult = SFXSlider.value; //commenting this out because I think we'll use several audio sources and it may be easier to set them one by one
        SoundManager.Instance.UI.volume = SFXSlider.value;
        Debug.Log(SFXSlider.value);
        if (canPlaySound)
        {
            SoundManager.Instance.Play_UI("SFX_UI_Slider");
            StartCoroutine(SoundCoolDown());
        }
    }

    /// <summary>
    /// Change the background music value
    /// </summary>
    public void MusicOnValueChange()
    {
        SoundManager.Instance.MusicMult = MusicSlider.value;
        SoundManager.Instance.BGM.volume = MusicSlider.value * 0.5f;
        Debug.Log(MusicSlider.value);
    }

    private IEnumerator SoundCoolDown()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(soundCoolDownTime);
        canPlaySound = true;
    }
}
