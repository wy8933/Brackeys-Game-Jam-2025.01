using System.Collections;
using UnityEngine;

public class FireplaceController : MonoBehaviour
{
    [Header("Log Settings")]
    [Tooltip("Number of logs required to trigger the ghost event")]
    public int maxLog = 5;

    [Tooltip("Minimum number of logs needed to avoid darkness")]
    public int minLog = 1;

    [SerializeField]
    private int _currentLogCount = 0;
    private bool _fireGhostTriggered = false;
    private bool _darkGhostTriggered = false;

    public void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Log"))
        {
            collision.GetComponent<Log>().StartBurn();
            _currentLogCount++;

            // If log count reaches or exceeds threshold and ghost event hasn't yet been triggered, trigger it
            if (_currentLogCount >= maxLog && !_fireGhostTriggered)
            {
                _fireGhostTriggered = true;
                if (EventManager.Instance != null)
                {
                    // Trigger the ghost event for the fireplace
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceStage1);
                }
            }

            if (_darkGhostTriggered && _currentLogCount > minLog)
            {
                _darkGhostTriggered = false;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.DarknessRepel);
                }
            }
            Debug.Log("Log Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Log"))
        {
            collision.GetComponent<Log>().StopBurn();
            _currentLogCount--;

            if (_fireGhostTriggered && _currentLogCount < maxLog)
            {
                _fireGhostTriggered = false;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceRepel);
                }
            }

            if (!_darkGhostTriggered && _currentLogCount < minLog)
            {
                _darkGhostTriggered = true;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.DarknessStage1);
                }
            }
            Debug.Log("Log Exit");
        }
        
    }
    public void OnLogBurn()
    {
        if (_currentLogCount > 0)
        {
            _currentLogCount--;

            // If a ghost event had been triggered and the count is now below threshold
            if (_fireGhostTriggered && _currentLogCount < maxLog)
            {
                _fireGhostTriggered = false;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.FireplaceRepel);
                }
            }

            if (!_darkGhostTriggered && _currentLogCount < minLog)
            {
                _darkGhostTriggered = true;
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.DarknessStage1);
                }
            }
        }
    }
}
