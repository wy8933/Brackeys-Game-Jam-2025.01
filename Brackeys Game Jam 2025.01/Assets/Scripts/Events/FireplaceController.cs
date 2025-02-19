using System.Collections;
using UnityEngine;

public class FireplaceController : MonoBehaviour
{
    [Header("Log Settings")]
    [Tooltip("Number of logs required within the time window to trigger the ghost event")]
    public int logThreshold = 3;

    private int currentLogCount = 0;
    private bool ghostTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Log"))
        {
            collision.GetComponent<Log>().startBurn = true;
            currentLogCount++;

            // If log count reaches or exceeds threshold and ghost event hasn't yet been triggered, trigger it.
            if (currentLogCount >= logThreshold && !ghostTriggered)
            {
                ghostTriggered = true;
                if (EventManager.Instance != null)
                {
                    // Trigger the ghost event for the fireplace
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceStage1);
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
            if (ghostTriggered && currentLogCount < logThreshold)
            {
                ghostTriggered = false;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceRepel);
                }
            }
        }
    }
}
