using UnityEngine;

public class Curtain : CoveringObject
{
    private const float LERP_TIME = .3f;
    public float coverTime;
    private float currentTime;
    private bool isCovering;
    
    private Vector2 _origin;
    private Vector2 velocity = Vector2.zero;
    public Vector2 coverPoint;

    void Start()
    {
        _origin = transform.position;
    }

    public void SetCover()
    {
        isCovering = true;
    }
    void Update()
    {
        if (currentTime < coverTime && isCovering)
        {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector2.SmoothDamp(transform.position, coverPoint, ref velocity, LERP_TIME);
        }
        else
        {
            isCovering = false;
            currentTime = 0;
            transform.position = Vector2.SmoothDamp(transform.position, _origin, ref velocity, LERP_TIME);
        }
    }
}
