using UnityEngine;

public class HoverTrigger : MonoBehaviour
{
    [Tooltip("Game time the mouse must hover over this object before triggering the ghost event")]
    public float hoverThreshold = 3f;

    private float hoverStartTime = 0f;
    private bool isHovering = false;
    private bool eventTriggered = false;

    void Update()
    {
        if (isHovering && !eventTriggered && GameTimer.Instance != null)
        {
            float currentGameTime = GameTimer.Instance.GetTimeElapsed();
            if (currentGameTime - hoverStartTime >= hoverThreshold)
            {
                eventTriggered = true;
                // Trigger the "The Rule" ghost event 
                if (EventManager.Instance != null)
                {
                    EventManager.Instance.TriggerGhostEventExternally(GameEvent.RuleGhostStage1);
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        isHovering = true;
        // Record the game time when the mouse starts hovering
        if (GameTimer.Instance != null)
        {
            hoverStartTime = GameTimer.Instance.GetTimeElapsed();
        }
    }

    private void OnMouseExit()
    {
        isHovering = false;
        eventTriggered = false;
        EventManager.Instance.TriggerGhostEventExternally(GameEvent.RuleGhostRepel);
    }
}
