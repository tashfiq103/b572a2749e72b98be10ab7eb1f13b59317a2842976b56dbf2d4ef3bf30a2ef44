
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MatchingCardController : MonoBehaviour
{
    #region  Public Variables

    public int Score{get; private set;} = 0;
    public int ComboStack {get; private set;} = 1;


    public  Action<MatchingCardComponent> OnFlippedStartedEvent;
    public  Action<MatchingCardComponent> OnFlippedEndedEvent;
    public  Action<MatchingCardComponent> OnUnflippedStartedEvent;
    public  Action<MatchingCardComponent> OnUnflippedEndedEvent;
    public  Action<MatchingCardComponent> OnDissolveStartedEvent;
    public  Action<MatchingCardComponent, float> OnDissolvingEvent;
    public  Action<MatchingCardComponent> OnDissolvingCompletedEvent;


    public event Action<int> OnScoreUpdatedEvent;
    public event Action<int> OnComboStackUpdatedEvent;
    public event Action<float, float> OnTimerUpdatedEvent;

    [Header("SoundFX")]
    public SoundFXFeedback soundFXOnCardMatched;
    public SoundFXFeedback soundFXOnCardMissMatched;

    [Space(25)]
    public SoundFXFeedback soundFXOnLevelComplete;
    public SoundFXFeedback soundFXOnLevelFailed;

    #endregion

    #region Private Variables

    private bool _isTimerCanRun;
    private int _numberOfCardMatched;

    private Stack<MatchingCardComponent> _flippedCardsStack = new Stack<MatchingCardComponent>();

    private float _levelDuration;
    private float _remainingTimeToCompleteTheLevel;

    #endregion

    #region Internal Method

    private IEnumerator GameTimer()
    {
        yield return new WaitUntil(()=>
        {
           return GameManager.Instance.IsGameRunning;
        });

        _levelDuration                      = DataManager.Instance.LevelDataContainerReference.CurrentLevelDataReference.levelDuration;
        _remainingTimeToCompleteTheLevel    = _levelDuration;

        do
        {
            float deltaTime = Time.deltaTime;
            _remainingTimeToCompleteTheLevel -= deltaTime;
            _remainingTimeToCompleteTheLevel = Mathf.Clamp(_remainingTimeToCompleteTheLevel, 0, _levelDuration);

            OnTimerUpdatedEvent?.Invoke(_remainingTimeToCompleteTheLevel, _levelDuration);

            if(_remainingTimeToCompleteTheLevel <= 0)
            {
                GameManager.Instance.OnLevelEndedEvent?.TriggerEvent();
                GameManager.Instance.OnLevelFailedEvent?.TriggerEvent();
            }

            yield return new WaitForSeconds(deltaTime);

        }while(_isTimerCanRun);
    }

    #endregion

    #region Internal Callback

    private void OnGameRunningStateUpdatedCallback(bool value)
    {
        _isTimerCanRun = value;
    }

    private void OnLevelStartedCallback()
    {
        Score                   = 0;
        ComboStack              = 1;

        _numberOfCardMatched    = 0;        

        StartCoroutine(GameTimer());
    }

    private void OnLevelEndedCallback()
    {
        
    }

    private void OnLevelCompletedCallback()
    {
        DataManager.Instance.LevelDataContainerReference.UpdateNextLevelIndex();
        soundFXOnLevelComplete.TryPlaySound();
    }

    private void OnLevelFailedCallback()
    {
        soundFXOnLevelFailed.TryPlaySound();
    }

    private void OnFlippedStartedCallback(MatchingCardComponent value)
    {
        
    }

    private void OnFlippedEndedCallback(MatchingCardComponent value)
    {
        if(_flippedCardsStack.Count > 0)
        {
            if(_flippedCardsStack.Peek() != value)
            {
                if(_flippedCardsStack.Peek().CardMatchData == value.CardMatchData)
                {
                    value.TryDissolve(OnDissolvingEvent, OnDissolvingCompletedEvent);
                    _flippedCardsStack.Pop().TryDissolve(OnDissolvingEvent, OnDissolvingCompletedEvent);

                    Score += 2 * ComboStack;
                    OnScoreUpdatedEvent?.Invoke(Score);
                    ComboStack++;

                    soundFXOnCardMatched.TryPlaySound();
                }
                else
                {
                    value.TryUnflip();
                    while(_flippedCardsStack.Count > 0)
                    {
                        _flippedCardsStack.Pop().TryUnflip();
                    }

                    ComboStack = 1;

                    soundFXOnCardMissMatched.TryPlaySound();
                }
                OnComboStackUpdatedEvent?.Invoke(ComboStack);
            }
            
        }
        else
        {
            if(!_flippedCardsStack.Contains(value))
                _flippedCardsStack.Push(value);
        }
    }

    private void OnUnflippedStartedCallback(MatchingCardComponent value)
    {
        
    }   

    private void OnUnflippedEndedCallback(MatchingCardComponent value)
    {
        if(_flippedCardsStack.Count > 0)
        {
            if(_flippedCardsStack.Peek() == value)
            {
                _flippedCardsStack.Pop();
            }
        }
    }


    private void OnDissolveStartedCallback(MatchingCardComponent value)
    {
        _numberOfCardMatched++;
        if(GameManager.Instance.IsGameRunning
        && _numberOfCardMatched == DataManager.Instance.LevelDataContainerReference.CurrentLevelDataReference.GridDatas.Length)
        {
            IEnumerator Delay()
            {
                GameManager.Instance.OnLevelEndedEvent.TriggerEvent();
                yield return new WaitForSeconds(.5f);
                GameManager.Instance.OnLevelCompletedEvent.TriggerEvent();
            }
            
            StartCoroutine(Delay());
        }
    }

    private void OnDissolvingCallback(MatchingCardComponent value, float progression)
    {
        
    }

    private void OnDissolvingCompletedCallback(MatchingCardComponent value)
    {
      
    }


    #endregion


    #region Unity Method

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStartedEvent.RegisterEvent(gameObject, OnLevelStartedCallback);
        GameManager.Instance.OnLevelEndedEvent.RegisterEvent(gameObject, OnLevelEndedCallback);

        GameManager.Instance.OnLevelCompletedEvent.RegisterEvent(gameObject, OnLevelCompletedCallback);
        GameManager.Instance.OnLevelFailedEvent.RegisterEvent(gameObject, OnLevelFailedCallback);

        GameManager.Instance.OnGameRunningEvent += OnGameRunningStateUpdatedCallback;

        OnFlippedStartedEvent += OnFlippedStartedCallback;
        OnFlippedEndedEvent += OnFlippedEndedCallback;

        OnUnflippedStartedEvent += OnUnflippedStartedCallback;
        OnUnflippedEndedEvent += OnUnflippedEndedCallback;

        OnDissolveStartedEvent += OnDissolveStartedCallback;
        OnDissolvingEvent += OnDissolvingCallback;
        OnDissolvingCompletedEvent += OnDissolvingCompletedCallback;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelStartedEvent.UnregisterEvent(gameObject);
        GameManager.Instance.OnLevelEndedEvent.UnregisterEvent(gameObject);

        GameManager.Instance.OnLevelCompletedEvent.UnregisterEvent(gameObject);
        GameManager.Instance.OnLevelFailedEvent.UnregisterEvent(gameObject);

        GameManager.Instance.OnGameRunningEvent -= OnGameRunningStateUpdatedCallback;

        OnFlippedStartedEvent -= OnFlippedStartedCallback;
        OnFlippedEndedEvent -= OnFlippedEndedCallback;

        OnUnflippedStartedEvent -= OnUnflippedStartedCallback;
        OnUnflippedEndedEvent -= OnUnflippedEndedCallback;

        OnDissolveStartedEvent -= OnDissolveStartedCallback;
        OnDissolvingEvent -= OnDissolvingCallback;
        OnDissolvingCompletedEvent -= OnDissolvingCompletedCallback;
    }

    #endregion
}
