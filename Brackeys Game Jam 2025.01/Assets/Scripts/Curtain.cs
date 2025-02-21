using UnityEngine;

public class Curtain : CoveringObject
{
    private const float LERP_TIME = .01f;
    private Vector3 _origin;

    void Start()
    {
        _origin = transform.position;
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, _origin, LERP_TIME);
    }
}
