using UnityEngine;

[System.Serializable]
public class TimedEvent
{
    [Tooltip("Time (in seconds) after the start of the game when this event should trigger")]
    public float triggerTime;

    [Tooltip("Select the event to trigger")]
    public GameEvent eventName;

    [HideInInspector]
    public bool hasTriggered = false;
}