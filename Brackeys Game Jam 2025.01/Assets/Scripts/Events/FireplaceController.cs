using System.Collections;
using UnityEngine;

public class FireplaceController : MonoBehaviour
{
    [Header("Log Settings")]
    [Tooltip("Number of logs required to trigger the ghost event")]
    public int maxLog = 5;

    [Tooltip("Minimum number of logs needed to avoid darkness")]
    public int minLog = 1;

    private int currentLogCount = 3;
    private bool ghostTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Log"))
        {
            collision.GetComponent<Log>().startBurn = true;
            currentLogCount++;

            // If log count reaches or exceeds threshold and ghost event hasn't yet been triggered, trigger it.
            if (currentLogCount >= maxLog && !ghostTriggered)
            {
                ghostTriggered = true;
                if (EventManager.Instance != null)
                {
                    // Trigger the ghost event for the fireplace
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceStage1);
                }
            }

            if (ghostTriggered && currentLogCount > minLog)
            {
                ghostTriggered = false;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.DarknessRepel);
                }
            }
        }
    }

    public void OnLogBurn()
    {
        if (currentLogCount > 0)
        {
            currentLogCount--;

            // If a ghost event had been triggered and the count is now below threshold
            if (ghostTriggered && currentLogCount < maxLog)
            {
                ghostTriggered = false;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceRepel);
                }
            }

            if (!ghostTriggered && currentLogCount < minLog)
            {
                ghostTriggered = true;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.DarknessStage1);
                }
            }
        }
    }
}
