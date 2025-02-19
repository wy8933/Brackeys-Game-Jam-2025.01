using UnityEngine;

[System.Serializable]
public class GameEventData
{
    [Tooltip("Time after the previous stage event when this event should trigger")]
    public float nextStageTime;

    [Tooltip("Select the event to trigger")]
    public GameEvent eventName;

    [HideInInspector]
    public bool hasTriggered = false;
}