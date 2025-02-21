using UnityEngine;

public class ChocolateBar : MonoBehaviour
{
    [Tooltip("Indicates whether the chocolate bar is open for eating")]
    public bool isOpen = false;

    [Tooltip("Sprites representing the chocolate bar states")]
    public Sprite[] chocolateStates;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Tracks the current state
    private int currentPieceIndex = 0;

    [Tooltip("Time interval between triggering the Rug Monster when the bar is open.")]
    public float rugMonsterTriggerInterval = 30f;
    private float lastRugMonsterTriggerTime = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize with the whole chocolate bar sprite
        if (chocolateStates != null && chocolateStates.Length > 0)
        {
            spriteRenderer.sprite = chocolateStates[0];
        }
    }

    private void Update()
    {
        // When the chocolate bar is open, check if it's time to trigger the Rug Monster event
        if (isOpen && GameTimer.Instance != null && EventManager.Instance != null)
        {
            float currentTime = GameTimer.Instance.GetTimeElapsed();
            if (currentTime - lastRugMonsterTriggerTime >= rugMonsterTriggerInterval)
            {
                // Trigger Rug Monster Stage 1 event
                EventManager.Instance.TriggerGhostEventExternally(GameEvent.RugMonsterStage1);
                lastRugMonsterTriggerTime = currentTime;
            }
        }
    }

    /// <summary>
    /// Opens the chocolate bar so that it can be eaten
    /// </summary>
    public void OpenBar()
    {
        isOpen = true;
        Debug.Log("Chocolate bar is now open!");
    }

    /// <summary>
    /// Simulates eating a piece of the chocolate bar by changing its sprite
    /// </summary>
    public void EatPiece()
    {
        if (!isOpen)
        {
            Debug.LogWarning("Cannot eat! The chocolate bar is closed. Open it first.");
            return;
        }

        // Check if there are remaining pieces to eat
        if (currentPieceIndex < chocolateStates.Length - 1)
        {
            currentPieceIndex++;
            spriteRenderer.sprite = chocolateStates[currentPieceIndex];
            Debug.Log("A piece of the chocolate bar has been eaten, current piece left: " + currentPieceIndex);
        }
        else
        {
            Debug.Log("The chocolate bar is fully eaten!");
        }
    }
}
