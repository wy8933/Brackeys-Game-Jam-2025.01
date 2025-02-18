using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameEvent
{
    EndNight,
    
    // WindowGhost events
    WindowGhostStage1,
    WindowGhostStage2,
    WindowGhostStage3,
    WindowGhostStage4,
    WindowGhostRepel,

    // TVGhost events
    TVGhostStage1,
    TVGhostStage2,
    TVGhostStage3,
    TVGhostRepel,

    // RuleGhost events
    RuleGhostStage1,
    RuleGhostStage2,
    RuleGhostRepel,

    // Uninvited events
    UninvitedStage1,
    UninvitedStage2,
    UninvitedStage3,
    UninvitedRepel,

    // HungryGhost events
    HungryGhostStage1,
    HungryGhostStage2,
    HungryGhostStage3,
    HungryGhostRepel,

    // Darkness events
    DarknessStage1,
    DarknessStage2,
    DarknessRepel,

    // Fireplace events
    FireplaceStage1,
    FireplaceStage2,
    FireplaceStage3,
    FireplaceRepel
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Tooltip("List of events to trigger at specific times")]
    public List<TimedEvent> timedEvents = new List<TimedEvent>();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!GameTimer.Instance) return;

        float elapsedTime = GameTimer.Instance.GetTimeElapsed();

        // Loop through each timed event
        foreach (TimedEvent timedEvent in timedEvents)
        {
            if (!timedEvent.hasTriggered && elapsedTime >= timedEvent.triggerTime)
            {
                timedEvent.hasTriggered = true;
                TriggerEvent(timedEvent.eventName);
            }
        }
    }

    /// <summary>
    /// Calls the corresponding method based on the enum event
    /// </summary>
    /// <param name="gameEvent">The event to trigger</param>
    private void TriggerEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvent.EndNight:
                EndNight();
                break;
            default:
                Debug.LogWarning("No event found for: " + gameEvent);
                break;
        }
    }

    private void EndNight()
    {
        Debug.Log("Night has ended!");
    }


    /// <summary>
    /// Called by CoveringObject when a coverable object is fully covered
    /// </summary>
    /// <param name="coverable">The coverable GameObject that is completely covered</param>
    public void OnCovered(GameObject coverable)
    {
        Debug.Log($"{coverable.name} is completely covered!");
    }
}
