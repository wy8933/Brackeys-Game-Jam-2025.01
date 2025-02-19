using UnityEngine;

public class GhostAutoTrigger : MonoBehaviour
{
    [Tooltip("Time interval of a ghost event is automatically triggered")]
    public float triggerInterval = 60f; 

    [Tooltip("List of ghost events to trigger")]
    public GameEvent[] autoTriggerGhostEvents = new GameEvent[]
    {
        GameEvent.WindowGhostStage1,
        GameEvent.TVGhostStage1,
        GameEvent.UninvitedStage1,
    };

    private float nextTriggerTime;

    private void Start()
    {
        // Set the first auto-trigger time
        nextTriggerTime = triggerInterval;
    }

    private void Update()
    {
        if (GameTimer.Instance == null || EventManager.Instance == null)
            return;

        float elapsedTime = GameTimer.Instance.GetTimeElapsed();

        // Check if it's time to trigger a ghost event
        if (elapsedTime >= nextTriggerTime)
        {
            // Randomly select one ghost event from the list
            int index = Random.Range(0, autoTriggerGhostEvents.Length);
            GameEvent chosenEvent = autoTriggerGhostEvents[index];

            Debug.Log($"Reached {nextTriggerTime} seconds, triggering {chosenEvent}");

            // Trigger the ghost event with the EventManager
            EventManager.Instance.TriggerGhostEventExternally(chosenEvent);

            // Schedule the next event
            nextTriggerTime += triggerInterval;
        }
    }
}
