using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

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

    /// <summary>
    /// Called by CoveringObject when a coverable object is fully covered
    /// </summary>
    /// <param name="coverable">The coverable GameObject that is completely covered</param>
    public void OnCovered(GameObject coverable)
    {
        Debug.Log($"{coverable.name} is completely covered!");
    }
}
