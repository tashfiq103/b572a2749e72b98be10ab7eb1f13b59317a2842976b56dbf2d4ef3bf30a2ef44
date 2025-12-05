using System;
using UnityEngine;

[DefaultExecutionOrder(-900)]
public class GameManager : MonoBehaviour
{
    #region Public Variables

    public static GameManager Instance{get; private set;}
    public bool IsGameRunning{get; private set;} = false;

    [Header("Managers & Controllers")]
    public MatchingCardSpawner matchingCardSpawner;
    public MatchingCardController matchingCardController;
   

    

    public event Action<bool> OnGameRunningEvent;

    [Header("Event")]
    public GameEventData OnLevelDataLoadedEvent;
    public GameEventData OnLevelStartedEvent;
    public GameEventData OnLevelEndedEvent;
    public GameEventData OnLevelCompletedEvent;
    public GameEventData OnLevelFailedEvent;

    #endregion

    #region Internal Callback

    private void OnLevelStartedCallback()
    {
        IsGameRunning = true;
        OnGameRunningEvent?.Invoke(IsGameRunning);   
    }

    private void OnLevelEndedCallback()
    {
        IsGameRunning = false;
        OnGameRunningEvent?.Invoke(IsGameRunning);  
    }

    #endregion

    #region Unity Callback

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        OnLevelStartedEvent.RegisterEvent(gameObject, OnLevelStartedCallback);
        OnLevelEndedEvent.RegisterEvent(gameObject, OnLevelEndedCallback);
    }

    private void OnDisable()
    {
        OnLevelStartedEvent.UnregisterEvent(gameObject);
        OnLevelEndedEvent.UnregisterEvent(gameObject);
    }


    #endregion
}
