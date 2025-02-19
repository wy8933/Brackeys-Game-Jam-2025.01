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
        if (GameTimer.Instance != null)
        {
            startTime = GameTimer.Instance.GetTimeElapsed();
        }
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

            Destroy(gameObject);
        }
    }
}
