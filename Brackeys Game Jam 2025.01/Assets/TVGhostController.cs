using UnityEngine;

public class TVGhostController : MonoBehaviour
{
    public int targetAntennaIndex;
    void Start()
    {
        Reset();
    }

    void Update()
    {
        if (TVAntenna.Instance)
        {
            if (targetAntennaIndex == TVAntenna.Instance.index)
            {
                EventManager.Instance.TriggerGhostEventExternally(GameEvent.TVGhostRepel);
                Reset();
            }
        }
    }

    public void Reset()
    {
        targetAntennaIndex = Random.Range(0, 5);
    }
}
