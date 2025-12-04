
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    #endregion

    #region Private Variables

    private Stack<MatchingCardComponent> _flippedCardsStack = new Stack<MatchingCardComponent>();

    #endregion

    #region Internal Callback

    private void OnLevelStartedCallback()
    {
        Score       = 0;
        ComboStack  = 1;
    }

    private void OnLevelEndedCallback()
    {
        
    }

    private void OnFlippedStartedCallback(MatchingCardComponent value)
    {
        
    }

    private void OnFlippedEndedCallback(MatchingCardComponent value)
    {
        if(_flippedCardsStack.Count > 0)
        {
            if(_flippedCardsStack.Peek().CardMatchData == value.CardMatchData)
            {
                value.TryDissolve(OnDissolvingEvent, OnDissolvingCompletedEvent);
                _flippedCardsStack.Pop().TryDissolve(OnDissolvingEvent, OnDissolvingCompletedEvent);

                Score += 2 * ComboStack;
                OnScoreUpdatedEvent?.Invoke(Score);
                ComboStack++;
            }
            else
            {
                value.TryUnflip();
                while(_flippedCardsStack.Count > 0)
                {
                    _flippedCardsStack.Pop().TryUnflip();
                }

                ComboStack = 1;
            }

            OnComboStackUpdatedEvent?.Invoke(ComboStack);
        }else
        {
            _flippedCardsStack.Push(value);
        }
    }

    private void OnUnflippedStartedCallback(MatchingCardComponent value)
    {
        
    }   

    private void OnUnflippedEndedCallback(MatchingCardComponent value)
    {
        
    }


    private void OnDissolveStartedCallback(MatchingCardComponent value)
    {
        
    }

    private void OnDissolvingCallback(MatchingCardComponent value, float progression)
    {
        
    }

    private void OnDissolvingCompletedCallback(MatchingCardComponent value)
    {
      
    }


    #endregion

    #region Private Variables


    #endregion

    #region Unity Method

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStartedEvent.RegisterEvent(gameObject, OnLevelStartedCallback);
        GameManager.Instance.OnLevelEndedEvent.RegisterEvent(gameObject, OnLevelEndedCallback);

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
