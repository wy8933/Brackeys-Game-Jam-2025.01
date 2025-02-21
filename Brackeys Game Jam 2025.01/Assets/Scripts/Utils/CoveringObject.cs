using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class CoveringObject : MonoBehaviour
{
    [Tooltip("Tag for objects that can be covered")]
    public string coverableTag = "Coverable";

    public EventManager eventManager;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        // Find the event manager if not assigned
        if (eventManager == null)
        {
            eventManager = FindFirstObjectByType<EventManager>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Only objects tagged as coverable.
        if (!collision.CompareTag(coverableTag))
            return;

        if (IsFullyContained(collision)) 
        { 
            eventManager?.OnCovered(collision.gameObject);
            eventManager?.TriggerGhostEventExternally(GameEvent.UninvitedRepel);
        }
    }

    /// <summary>
    /// Determines if all four corners of the coverable object lie inside of covering object
    /// </summary>
    /// <param name="coverableCollider">The collider of the coverable object</param>
    /// <returns>True if fully contained otherwise false</returns>
    private bool IsFullyContained(Collider2D coverableCollider)
    {
        Bounds bounds = coverableCollider.bounds;
        Vector2[] corners = new Vector2[4]
        {
            new Vector2(bounds.min.x, bounds.min.y),
            new Vector2(bounds.min.x, bounds.max.y),
            new Vector2(bounds.max.x, bounds.min.y),
            new Vector2(bounds.max.x, bounds.max.y)
        };

        // Check each corner
        foreach (Vector2 corner in corners)
        {
            if (!_collider.OverlapPoint(corner))
            {
                return false;
            }
        }
        return true;
    }
}
