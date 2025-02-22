using UnityEngine;

public class Curtain : CoveringObject
{
    private float currentTime;
    private bool isCovering;

    public SpriteRenderer curtainSprite;

    public float revertTime = 5f;

    private float lastClickGameTime;
    private bool isClicked = false;

    void Start()
    {

        if (curtainSprite != null)
        {
            curtainSprite.enabled = false;
        }
    }

    public void SetCover()
    {
        isCovering = !isCovering;
    }

    public float GetCurrentCoverTime()
    {
        return currentTime;
    }

    void Update()
    {
        if (isClicked && GameTimer.Instance != null)
        {
            float currentGameTime = GameTimer.Instance.GetTimeElapsed();
            if (currentGameTime - lastClickGameTime >= revertTime)
            {
                curtainSprite.enabled = false;
                isClicked =false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isClicked && curtainSprite != null)
        {
            curtainSprite.enabled = true;
            isClicked = true;

            lastClickGameTime = GameTimer.Instance.GetTimeElapsed();

            EventManager.Instance.TriggerGhostEventExternally(GameEvent.WindowGhostRepel);
        }
    }
}
