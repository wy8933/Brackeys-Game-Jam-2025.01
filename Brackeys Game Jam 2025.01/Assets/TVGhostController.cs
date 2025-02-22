using UnityEngine;

public class TVGhostController : MonoBehaviour
{
    public int targetAntennaIndex;
    void Start()
    {
        targetAntennaIndex = Random.Range(0,5);
    }

    void Update()
    {
        if (TVAntenna.Instance)
        {
            if (targetAntennaIndex == TVAntenna.Instance.index)
            {
                EventManager.Instance.TriggerGhostEventExternally(GameEvent.TVGhostRepel);
            }
        }
    }
}
