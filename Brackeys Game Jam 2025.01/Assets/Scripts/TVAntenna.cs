using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TVAntenna : MonoBehaviour
{
    public static TVAntenna Instance;
    public int index;

    public List<Vector2> listPositions;
    
    public LineRenderer antennaLine;
    public Transform antennaTip;
    public Transform antennaBase;
    void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        
        antennaLine.enabled = true;
        antennaTip = transform;
    }

    void Update()
    {
        antennaLine.SetPosition(0, antennaTip.position);
        antennaLine.SetPosition(1, antennaBase.position);
    }

    public void ChangeAntennaPosition()
    {
        index++;
        if (index > listPositions.Count - 1)
            index = 0;

        antennaTip.localPosition = listPositions[index];
    }
}
