using System;
using UnityEngine;

public class HungryGhostPortraitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (EventManager.Instance)
            {
                EventManager.Instance.TriggerGhostEventExternally(GameEvent.HungryGhostRepel);
                Destroy(other.gameObject);
            }
        }
    }
}
