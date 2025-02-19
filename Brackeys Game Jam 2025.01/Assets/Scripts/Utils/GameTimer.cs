using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private const float GameDuration = 360f; //6min
    public static GameTimer Instance;

    private float _elapsedTime;
    
    [SerializeField] private TextMeshProUGUI clockText;
    public float GetTimeElapsed() { return _elapsedTime; }
    
    void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        
        _elapsedTime = 0f;
    }


    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (clockText)
        {
            float progress = _elapsedTime / GameDuration;
            float hours = progress * 6f; 
            int minutes = Mathf.FloorToInt((hours - Mathf.Floor(hours)) * 60);
            clockText.SetText($"{Mathf.Floor(hours)}:{minutes:00} AM");
        }
    }

}
