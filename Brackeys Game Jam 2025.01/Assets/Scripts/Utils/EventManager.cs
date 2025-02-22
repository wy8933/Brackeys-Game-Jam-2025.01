using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

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
    FireplaceRepel,


    //Rug Monster events
    RugMonsterStage1,
    RugMonsterStage2,
    RugMonsterStage3,
    RugMonsterRepel
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private Dictionary<string, bool> ghostSeriesTriggered = new Dictionary<string, bool>();

    public float autoProgressMultiplier = 1;

    private Dictionary<GameEvent, GameEventData> ghostProgressionMapping;

    [Header("Items")]
    public Rug rug;

    [Header("Reference")]
    public Animator uninvitedAnimator;
    public GameObject door;

    public Animator darknessAnimator;

    public Animator rugAnimator;

    public Animator portraitAnimator;

    public Animator windowAnimator;

    public Animator fireAnimator;

    public Animator TVAnimator;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            string[] series = { "WindowGhost", "TVGhost", "RuleGhost", "Uninvited", "HungryGhost", "Darkness", "Fireplace", "RugMoster" };
            foreach (string s in series)
            {
                ghostSeriesTriggered[s] = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }



        ghostProgressionMapping = new Dictionary<GameEvent, GameEventData>()
    {
        // WindowGhost progression
        { GameEvent.WindowGhostStage1, new GameEventData  { eventName = GameEvent.WindowGhostStage2, nextStageTime = 60f * autoProgressMultiplier } },
        { GameEvent.WindowGhostStage2, new GameEventData  { eventName = GameEvent.WindowGhostStage3, nextStageTime = 60f * autoProgressMultiplier } },
        { GameEvent.WindowGhostStage3, new GameEventData  { eventName = GameEvent.WindowGhostStage4, nextStageTime = 60f * autoProgressMultiplier } },

        // TVGhost progression
        { GameEvent.TVGhostStage1, new GameEventData  { eventName = GameEvent.TVGhostStage2, nextStageTime = 45f * autoProgressMultiplier } },
        { GameEvent.TVGhostStage2, new GameEventData  { eventName = GameEvent.TVGhostStage3, nextStageTime = 45f * autoProgressMultiplier } },

        // RuleGhost progression
        { GameEvent.RuleGhostStage1, new GameEventData  { eventName = GameEvent.RuleGhostStage2, nextStageTime = 50f * autoProgressMultiplier } },

        // Uninvited progression
        { GameEvent.UninvitedStage1, new GameEventData  { eventName = GameEvent.UninvitedStage2, nextStageTime = 55f * autoProgressMultiplier } },
        { GameEvent.UninvitedStage2, new GameEventData  { eventName = GameEvent.UninvitedStage3, nextStageTime = 55f * autoProgressMultiplier } },

        // HungryGhost progression
        { GameEvent.HungryGhostStage1, new GameEventData  { eventName = GameEvent.HungryGhostStage2, nextStageTime = 50f * autoProgressMultiplier } },
        { GameEvent.HungryGhostStage2, new GameEventData  { eventName = GameEvent.HungryGhostStage3, nextStageTime = 50f * autoProgressMultiplier } },

        // Darkness progression
        { GameEvent.DarknessStage1, new GameEventData  { eventName = GameEvent.DarknessStage2, nextStageTime = 40f * autoProgressMultiplier } },

        // Fireplace progression
        { GameEvent.FireplaceStage1, new GameEventData  { eventName = GameEvent.FireplaceStage2, nextStageTime = 60f * autoProgressMultiplier } },
        { GameEvent.FireplaceStage2, new GameEventData  { eventName = GameEvent.FireplaceStage3, nextStageTime = 60f * autoProgressMultiplier } },

        // RugMonster progression
        { GameEvent.RugMonsterStage1, new GameEventData{ eventName = GameEvent.RugMonsterStage2, nextStageTime = 30f * autoProgressMultiplier } },
        { GameEvent.RugMonsterStage2, new GameEventData{ eventName = GameEvent.RugMonsterStage3, nextStageTime = 30f * autoProgressMultiplier } },
    };
    }


    /// <summary>
    /// Calls the corresponding method based on the enum event
    /// </summary>
    /// <param name="eventName">The event to trigger</param>
    private void TriggerEvent(GameEvent gameEvent)
    {
        string series = GetGhostSeries(gameEvent);

        // If event is stage1 and the series is already triggered
        if (gameEvent.ToString().Contains("Stage1") && ghostSeriesTriggered.ContainsKey(series) && ghostSeriesTriggered[series])
        {
            Debug.Log($"{gameEvent} of ghost series '{series}' already triggered");
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

            // Rug Monster Event
            case GameEvent.RugMonsterStage1:
                ghostSeriesTriggered[series] = true;
                HandleRugMonsterStage1();
                break;
            case GameEvent.RugMonsterStage2:
                HandleRugMonsterStage2();
                break;
            case GameEvent.RugMonsterStage3:
                HandleRugMonsterStage3();
                break;
            case GameEvent.RugMonsterRepel:
                ResetGhostSeries(GetGhostSeries(GameEvent.RugMonsterRepel));
                HandleRugMonsterRepel();
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
        yield return new WaitForSeconds(nextTimedEvent.nextStageTime);
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
       if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("WindowGhost Stage 1 triggered!");

        if (windowAnimator != null)
        {
            windowAnimator.SetTrigger("Stage1");
        }
    }
    private void HandleWindowGhostStage2() 
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("WindowGhost Stage 2 triggered!");

        if (windowAnimator != null)
        {
            windowAnimator.SetTrigger("Stage2");
        }
    }
    private void HandleWindowGhostStage3() 
    {
        SoundManager.Instance.BGM_Intense.volume = 1.0f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,7800.0f,2.0f);
        Debug.Log("WindowGhost Stage 3 triggered!");

        if (windowAnimator != null)
        {
            windowAnimator.SetTrigger("Stage3");
        }
    }
    private void HandleWindowGhostStage4() 
    {
        SoundManager.Instance.BGM_Intense.volume = 1.1f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,9000.0f,2.0f);
        Debug.Log("WindowGhost Stage 4 triggered!");

        if (windowAnimator != null)
        {
            windowAnimator.SetTrigger("Stage4");
        }
    }
    private void HandleWindowGhostRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,3.0f);
        Debug.Log("WindowGhost Repel triggered!");

        if (windowAnimator != null)
        {
            windowAnimator.SetTrigger("Repel");
        }
    }
    #endregion

    #region TV Ghost
    private void HandleTVGhostStage1() 
    {
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("TVGhost Stage 1 triggered!"); 
    }
    private void HandleTVGhostStage2() 
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("TVGhost Stage 2 triggered!"); 
    }
    private void HandleTVGhostStage3() 
    {
        SoundManager.Instance.BGM_Intense.volume = 1.0f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,7800.0f,2.0f);
        Debug.Log("TVGhost Stage 3 triggered!"); 
    }
    private void HandleTVGhostRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,3.0f);
        Debug.Log("TVGhost Repel triggered!"); 
    }

    #endregion

    #region Rule Ghost
    private void HandleRuleGhostStage1() 
    {
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("RuleGhost Stage 1 triggered!"); 
    }
    private void HandleRuleGhostStage2() 
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("RuleGhost Stage 2 triggered!"); 
    }
    private void HandleRuleGhostRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,3.5f);
        Debug.Log("RuleGhost Repel triggered!"); 
    }
    #endregion

    #region Uninvited
    private void HandleUninvitedStage1() 
    {
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("Uninvited Stage 1 triggered!");

        if (uninvitedAnimator != null)
        {
            uninvitedAnimator.SetTrigger("UninvitedStage1");
        }
    }
    private void HandleUninvitedStage2() 
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("Uninvited Stage 2 triggered!");

        if (uninvitedAnimator != null)
        {
            uninvitedAnimator.SetTrigger("UninvitedStage2");
        }
    }
    private void HandleUninvitedStage3() 
    {
        door.SetActive(false);
        SoundManager.Instance.BGM_Intense.volume = 1.1f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,8000.0f,2.0f);
        Debug.Log("Uninvited Stage 3 triggered!");

        if (uninvitedAnimator != null)
        {
            uninvitedAnimator.SetTrigger("UninvitedStage3");
        }
    }
    private void HandleUninvitedRepel() 
    {
        Debug.Log("Uninvited Repel triggered!");
        door.SetActive(true);

        if (uninvitedAnimator != null)
        {
            uninvitedAnimator.SetTrigger("UninvitedRepel");
        }
    }
    #endregion

    #region HungryGhost
    // HungryGhost event methods
    private void HandleHungryGhostStage1() 
    {
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("HungryGhost Stage 1 triggered!");

        if (portraitAnimator != null)
        {
            portraitAnimator.SetTrigger("Stage1");
        }
    }
    private void HandleHungryGhostStage2() 
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("HungryGhost Stage 2 triggered!");

        if (portraitAnimator != null)
        {
            portraitAnimator.SetTrigger("Stage2");
        }
    }
    private void HandleHungryGhostStage3() 
    {
        SoundManager.Instance.BGM_Intense.volume = 1.1f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,8000.0f,2.0f);
        Debug.Log("HungryGhost Stage 3 triggered!");

        if (portraitAnimator != null)
        {
            portraitAnimator.SetTrigger("Stage3");
        }
    }
    private void HandleHungryGhostRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,2.0f);
        Debug.Log("HungryGhost Repel triggered!");

        if (portraitAnimator != null)
        {
            portraitAnimator.SetTrigger("Repel");
        }
    }
    #endregion

    #region Darkness
    // Darkness event methods
    private void HandleDarknessStage1() 
    {
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("Darkness Stage 1 triggered!");

        if (darknessAnimator != null)
        {
            darknessAnimator.SetTrigger("Stage1");
        }
    }
    private void HandleDarknessStage2() 
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("Darkness Stage 2 triggered!");

        if (darknessAnimator != null)
        {
            darknessAnimator.SetTrigger("Stage2");
        }
    }

    private void HandleDarknessRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,2.0f);
        Debug.Log("Darkness Repel triggered!");

        if (darknessAnimator != null)
        {
            darknessAnimator.SetTrigger("Repel");
        }
    }
    #endregion

    #region Fireplace
    // Fireplace event methods
    private void HandleFireplaceStage1() 
    { 
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("Fireplace Stage 1 triggered!"); 
    }
    private void HandleFireplaceStage2()
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("Fireplace Stage 2 triggered!"); 
    }
    private void HandleFireplaceStage3() 
    {
        SoundManager.Instance.BGM_Intense.volume = 1.1f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,8000.0f,2.0f);
        Debug.Log("Fireplace Stage 3 triggered!");
    }
    private void HandleFireplaceRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,2.0f);
        Debug.Log("Fireplace Repel triggered!");
    }
    #endregion

    #region Rug Monster
    private void HandleRugMonsterStage1()
    {
        if (!SoundManager.Instance.BGM_Intense.isPlaying)
        {
            SoundManager.Instance.BGM_Intense_LPF.cutoffFrequency = 1200.0f;
            SoundManager.Instance.playLoopingMusic("BGM_Intense_Synth",SoundManager.Instance.BGM_Intense, 0.9f);
        }
        Debug.Log("Rug Monster Stage 1 triggered");
        rug.StartRugMonster();

        if (rugAnimator != null)
        {
            rugAnimator.SetTrigger("Stage1");
        }
    }

    private void HandleRugMonsterStage2()
    {
        SoundManager.Instance.BGM_Intense.volume = 0.95f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,4800.0f,2.0f);
        Debug.Log("Rug Monster Stage 2 triggered");

        if (rugAnimator != null)
        {
            rugAnimator.SetTrigger("Stage2");
        }
    }

    private void HandleRugMonsterStage3()
    {
        SoundManager.Instance.BGM_Intense.volume = 1.1f;
        SoundManager.Instance.LerpLPFCutoff(SoundManager.Instance.BGM_Intense_LPF,6000.0f,2.0f);
        Debug.Log("Rug Monster Stage 3 triggered");

        if (rugAnimator != null)
        {
            rugAnimator.SetTrigger("Stage3");
        }
    }

    private void HandleRugMonsterRepel() 
    {
        SoundManager.Instance.FadeAudio(SoundManager.Instance.BGM_Intense,2.0f);
        Debug.Log("RugMonster Repel triggered!");

        if (rugAnimator != null)
        {
            rugAnimator.SetTrigger("Repel");
        }
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
