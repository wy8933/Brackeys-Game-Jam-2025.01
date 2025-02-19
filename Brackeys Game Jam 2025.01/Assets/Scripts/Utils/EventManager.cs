using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameEvent
{
    EndNight,
    
    // WindowGhost events
    WindowGhostStage1,
    WindowGhostStage2,
    WindowGhostStage3,
    WindowGhostStage4,
    WindowGhostRepel,

    // TVGhost events
    TVGhostStage1,
    TVGhostStage2,
    TVGhostStage3,
    TVGhostRepel,

    // RuleGhost events
    RuleGhostStage1,
    RuleGhostStage2,
    RuleGhostRepel,

    // Uninvited events
    UninvitedStage1,
    UninvitedStage2,
    UninvitedStage3,
    UninvitedRepel,

    // HungryGhost events
    HungryGhostStage1,
    HungryGhostStage2,
    HungryGhostStage3,
    HungryGhostRepel,

    // Darkness events
    DarknessStage1,
    DarknessStage2,
    DarknessRepel,

    // Fireplace events
    FireplaceStage1,
    FireplaceStage2,
    FireplaceStage3,
    FireplaceRepel
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    
    private Dictionary<string, bool> ghostSeriesTriggered = new Dictionary<string, bool>();

    private Dictionary<GameEvent, GameEventData> ghostProgressionMapping = new Dictionary<GameEvent, GameEventData>()
    {
        // WindowGhost progression
        { GameEvent.WindowGhostStage1, new GameEventData  { eventName = GameEvent.WindowGhostStage2, nextStageTime = 60f } },
        { GameEvent.WindowGhostStage2, new GameEventData  { eventName = GameEvent.WindowGhostStage3, nextStageTime = 60f } },
        { GameEvent.WindowGhostStage3, new GameEventData  { eventName = GameEvent.WindowGhostStage4, nextStageTime = 60f } },

        // TVGhost progression
        { GameEvent.TVGhostStage1, new GameEventData  { eventName = GameEvent.TVGhostStage2, nextStageTime = 45f } },
        { GameEvent.TVGhostStage2, new GameEventData  { eventName = GameEvent.TVGhostStage3, nextStageTime = 45f } },

        // RuleGhost progression
        { GameEvent.RuleGhostStage1, new GameEventData  { eventName = GameEvent.RuleGhostStage2, nextStageTime = 50f } },

        // Uninvited progression
        { GameEvent.UninvitedStage1, new GameEventData  { eventName = GameEvent.UninvitedStage2, nextStageTime = 55f } },
        { GameEvent.UninvitedStage2, new GameEventData  { eventName = GameEvent.UninvitedStage3, nextStageTime = 55f } },

        // HungryGhost progression
        { GameEvent.HungryGhostStage1, new GameEventData  { eventName = GameEvent.HungryGhostStage2, nextStageTime = 50f } },
        { GameEvent.HungryGhostStage2, new GameEventData  { eventName = GameEvent.HungryGhostStage3, nextStageTime = 50f } },

        // Darkness progression
        { GameEvent.DarknessStage1, new GameEventData  { eventName = GameEvent.DarknessStage2, nextStageTime = 40f } },

        // Fireplace progression
        { GameEvent.FireplaceStage1, new GameEventData  { eventName = GameEvent.FireplaceStage2, nextStageTime = 60f } },
        { GameEvent.FireplaceStage2, new GameEventData  { eventName = GameEvent.FireplaceStage3, nextStageTime = 60f } },
    };


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            string[] series = { "WindowGhost", "TVGhost", "RuleGhost", "Uninvited", "HungryGhost", "Darkness", "Fireplace" };
            foreach (string s in series)
            {
                ghostSeriesTriggered[s] = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Calls the corresponding method based on the enum event
    /// </summary>
    /// <param name="eventName">The event to trigger</param>
    private void TriggerEvent(GameEvent gameEvent)
    {
        string series = GetGhostSeries(gameEvent);

        // If event is not Stage1 and the series is already triggered
        if (!gameEvent.ToString().Contains("Stage1") && ghostSeriesTriggered.ContainsKey(series) && ghostSeriesTriggered[series])
        {
            Debug.Log($"Ghost series '{series}' already triggered");
            return;
        }

        switch (gameEvent)
        {
            case GameEvent.EndNight:
                EndNight();
                break;

            // WindowGhost events
            case GameEvent.WindowGhostStage1:
                ghostSeriesTriggered[series] = true;
                HandleWindowGhostStage1();
                break;
            case GameEvent.WindowGhostStage2:
                HandleWindowGhostStage2();
                break;
            case GameEvent.WindowGhostStage3:
                HandleWindowGhostStage3();
                break;
            case GameEvent.WindowGhostStage4:
                HandleWindowGhostStage4();
                break;
            case GameEvent.WindowGhostRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.WindowGhostRepel));
                HandleWindowGhostRepel();
                break;

            // TVGhost events
            case GameEvent.TVGhostStage1:
                ghostSeriesTriggered[series] = true;
                HandleTVGhostStage1();
                break;
            case GameEvent.TVGhostStage2:
                HandleTVGhostStage2();
                break;
            case GameEvent.TVGhostStage3:
                HandleTVGhostStage3();
                break;
            case GameEvent.TVGhostRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.TVGhostRepel));
                HandleTVGhostRepel();
                break;

            // RuleGhost events
            case GameEvent.RuleGhostStage1:
                ghostSeriesTriggered[series] = true;
                HandleRuleGhostStage1();
                break;
            case GameEvent.RuleGhostStage2:
                HandleRuleGhostStage2();
                break;
            case GameEvent.RuleGhostRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.RuleGhostRepel));
                HandleRuleGhostRepel();
                break;

            // Uninvited events
            case GameEvent.UninvitedStage1:
                ghostSeriesTriggered[series] = true;
                HandleUninvitedStage1();
                break;
            case GameEvent.UninvitedStage2:
                HandleUninvitedStage2();
                break;
            case GameEvent.UninvitedStage3:
                HandleUninvitedStage3();
                break;
            case GameEvent.UninvitedRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.UninvitedRepel));
                HandleUninvitedRepel();
                break;

            // HungryGhost events
            case GameEvent.HungryGhostStage1:
                ghostSeriesTriggered[series] = true;
                HandleHungryGhostStage1();
                break;
            case GameEvent.HungryGhostStage2:
                HandleHungryGhostStage2();
                break;
            case GameEvent.HungryGhostStage3:
                HandleHungryGhostStage3();
                break;
            case GameEvent.HungryGhostRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.HungryGhostRepel));
                HandleHungryGhostRepel();
                break;

            // Darkness events
            case GameEvent.DarknessStage1:
                ghostSeriesTriggered[series] = true;
                HandleDarknessStage1();
                break;
            case GameEvent.DarknessStage2:
                HandleDarknessStage2();
                break;
            case GameEvent.DarknessRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.DarknessRepel));
                HandleDarknessRepel();
                break;

            // Fireplace events
            case GameEvent.FireplaceStage1:
                ghostSeriesTriggered[series] = true;
                HandleFireplaceStage1();
                break;
            case GameEvent.FireplaceStage2:
                HandleFireplaceStage2();
                break;
            case GameEvent.FireplaceStage3:
                HandleFireplaceStage3();
                break;
            case GameEvent.FireplaceRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.FireplaceRepel));
                HandleFireplaceRepel();
                break;

            default:
                Debug.LogWarning("No event found for: " + gameEvent);
                break;
        }

        if (!gameEvent.ToString().Contains("Repel") && ghostProgressionMapping.TryGetValue(gameEvent, out GameEventData nextStageEvent))
        {
            StartCoroutine(AutoProgressGhost(nextStageEvent));
        }

    }
    public void TriggerGhostEventExternally(GameEvent ghostEvent)
    {
        TriggerEvent(ghostEvent);
    }

    /// <summary>
    /// Coroutine that waits for a specified time before auto-triggering the next ghost stage
    /// </summary>
    private IEnumerator AutoProgressGhost(GameEventData nextTimedEvent)
    {
        yield return new WaitForSeconds(nextTimedEvent.nextStageTime/10); // Divide the time by 10 for testing purpose, make sure to remove it
        Debug.Log("Auto-progressing to next ghost stage: " + nextTimedEvent.eventName);
        TriggerGhostEventExternally(nextTimedEvent.eventName);
    }

    private string GetGhostSeries(GameEvent gameEvent)
    {
        string name = gameEvent.ToString();
        int index = name.IndexOf("Stage");
        if (index < 0)
        {
            index = name.IndexOf("Repel");
        }
        return index >= 0 ? name.Substring(0, index) : name;
    }

    private void ResetGhostSeries(string series)
    {
        if (ghostSeriesTriggered.ContainsKey(series))
        {
            ghostSeriesTriggered[series] = false;
        }
    }

    private void EndNight()
    {
        Debug.Log("Night has ended!");
    }



    #region Ghost Events

    #region Window Ghost
    private void HandleWindowGhostStage1() 
    {
        Debug.Log("WindowGhost Stage 1 triggered!"); 
    }
    private void HandleWindowGhostStage2() 
    {
        Debug.Log("WindowGhost Stage 2 triggered!"); 
    }
    private void HandleWindowGhostStage3() 
    {
        Debug.Log("WindowGhost Stage 3 triggered!"); 
    }
    private void HandleWindowGhostStage4() 
    {
        Debug.Log("WindowGhost Stage 4 triggered!"); 
    }
    private void HandleWindowGhostRepel() 
    {
        Debug.Log("WindowGhost Repel triggered!"); 
    }
    #endregion

    #region TV Ghost
    private void HandleTVGhostStage1() 
    {
        Debug.Log("TVGhost Stage 1 triggered!"); 
    }
    private void HandleTVGhostStage2() 
    {
        Debug.Log("TVGhost Stage 2 triggered!"); 
    }
    private void HandleTVGhostStage3() 
    {
        Debug.Log("TVGhost Stage 3 triggered!"); 
    }
    private void HandleTVGhostRepel() 
    {
        Debug.Log("TVGhost Repel triggered!"); 
    }

    #endregion

    #region Rule Ghost
    private void HandleRuleGhostStage1() 
    {
        Debug.Log("RuleGhost Stage 1 triggered!"); 
    }
    private void HandleRuleGhostStage2() 
    {
        Debug.Log("RuleGhost Stage 2 triggered!"); 
    }
    private void HandleRuleGhostRepel() 
    {
        Debug.Log("RuleGhost Repel triggered!"); 
    }
    #endregion

    #region Uninvited
    private void HandleUninvitedStage1() 
    {
        Debug.Log("Uninvited Stage 1 triggered!"); 
    }
    private void HandleUninvitedStage2() 
    {
        Debug.Log("Uninvited Stage 2 triggered!"); 
    }
    private void HandleUninvitedStage3() 
    {
        Debug.Log("Uninvited Stage 3 triggered!"); 
    }
    private void HandleUninvitedRepel() 
    {
        Debug.Log("Uninvited Repel triggered!"); 
    }
    #endregion

    #region HungryGhost
    // HungryGhost event methods
    private void HandleHungryGhostStage1() 
    {
        Debug.Log("HungryGhost Stage 1 triggered!"); 
    }
    private void HandleHungryGhostStage2() 
    {
        Debug.Log("HungryGhost Stage 2 triggered!");
    }
    private void HandleHungryGhostStage3() 
    {
        Debug.Log("HungryGhost Stage 3 triggered!"); 
    }
    private void HandleHungryGhostRepel() 
    {
        Debug.Log("HungryGhost Repel triggered!"); 
    }
    #endregion

    #region Darkness
    // Darkness event methods
    private void HandleDarknessStage1() 
    {
        Debug.Log("Darkness Stage 1 triggered!");
    }
    private void HandleDarknessStage2() 
    {
        Debug.Log("Darkness Stage 2 triggered!"); 
    }
    private void HandleDarknessRepel() 
    {
        Debug.Log("Darkness Repel triggered!"); 
    }
    #endregion

    #region Fireplace
    // Fireplace event methods
    private void HandleFireplaceStage1() 
    { 
        Debug.Log("Fireplace Stage 1 triggered!"); 
    }
    private void HandleFireplaceStage2()
    {
        Debug.Log("Fireplace Stage 2 triggered!"); 
    }
    private void HandleFireplaceStage3() 
    {
        Debug.Log("Fireplace Stage 3 triggered!");
    }
    private void HandleFireplaceRepel() 
    {
        Debug.Log("Fireplace Repel triggered!");
    }
    #endregion
    
    #endregion


    /// <summary>
    /// Called by CoveringObject when a coverable object is fully covered
    /// </summary>
    /// <param name="coverable">The coverable GameObject that is completely covered</param>
    public void OnCovered(GameObject coverable)
    {
        Debug.Log($"{coverable.name} is completely covered!");
    }
}
