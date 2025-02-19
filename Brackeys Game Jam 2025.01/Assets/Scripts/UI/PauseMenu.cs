using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public bool isPaused;

    public GameObject pausePanel;

    private void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
