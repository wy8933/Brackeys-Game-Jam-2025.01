using System;
using UnityEngine;

public class WindowGhostController : MonoBehaviour
{
    private float timeBeforeRepel = 4f;
    private float currentTime;

    [SerializeField] private bool isCovered;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Curtain"))
        {
            isCovered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Curtain"))
        {
            isCovered = false;
        }
    }

    void Update()
    {
        if (!isCovered)
        {
            currentTime = 0;
            return;
        }

        currentTime += Time.deltaTime;
        if (currentTime >= timeBeforeRepel)
        {
            if (EventManager.Instance)
            {
                EventManager.Instance.TriggerGhostEventExternally(GameEvent.WindowGhostRepel);
            }
        }
    }
}
