using UnityEngine;

public class Log : MonoBehaviour
{
    [Tooltip("Duration before this log burns out")]
    public float burnDuration = 30f;

    private float startTime;
    public bool startBurn = false;

    private FireplaceController fireplaceController;

    void Start()
    {
        // Automatically find the FireplaceController in the scene
        fireplaceController = FindObjectOfType<FireplaceController>();
    }

    void Update()
    {
        if (!startBurn)
            return;

        float currentGameTime = GameTimer.Instance.GetTimeElapsed();
        if (currentGameTime - startTime >= burnDuration)
        {
            // Inform the FireplaceController that this log has burned
            if (fireplaceController != null)
            {
                fireplaceController.OnLogBurn();
            }

            SoundManager.Instance.SFX_Fireplace.volume = SoundManager.Instance.SFX_Fireplace.volume - 0.15f;
            Destroy(gameObject);
        }
    }

    public void StartBurn()
    {
        SoundManager.Instance.SFX_Fireplace.volume = SoundManager.Instance.SFX_Fireplace.volume + 0.15f;
        startBurn = true;
        if (GameTimer.Instance != null)
        {
            startTime = GameTimer.Instance.GetTimeElapsed();
        }
    }

    public void StopBurn() 
    {
        SoundManager.Instance.SFX_Fireplace.volume = SoundManager.Instance.SFX_Fireplace.volume - 0.15f;
        startBurn = false;

        float currentGameTime = GameTimer.Instance.GetTimeElapsed();
        burnDuration -= currentGameTime - startTime;
    }
}
