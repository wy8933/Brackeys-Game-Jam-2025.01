using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Portrait : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventManager.Instance != null)
        {
            // Trigger the Hungry Ghost Stage 1 event
            EventManager.Instance.TriggerGhostEventExternally(GameEvent.HungryGhostStage1);
        }
    }
}