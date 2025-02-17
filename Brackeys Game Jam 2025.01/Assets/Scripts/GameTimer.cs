using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private const float GameDuration = 360f;
    
    private float _elapsedTime;
    private bool _isNightOver;
    
    public TextMeshProUGUI clockText;
    void Start()
    {
        _elapsedTime = 0f;
    }

    void Update()
    {
        if (!_isNightOver)
        {
            _elapsedTime += Time.deltaTime;
            float progress = _elapsedTime / GameDuration;
            
            float hours = 12f + progress * 6f; 
            int minutes = Mathf.FloorToInt((hours - Mathf.Floor(hours)) * 60);
            clockText.SetText($"{Mathf.Floor(hours)}:{minutes:00} AM");
        
            if (_elapsedTime >= GameDuration)
            {
                EndNight();
            }
        }
    }

    private void EndNight()
    {
        _isNightOver = true;
    }
}
