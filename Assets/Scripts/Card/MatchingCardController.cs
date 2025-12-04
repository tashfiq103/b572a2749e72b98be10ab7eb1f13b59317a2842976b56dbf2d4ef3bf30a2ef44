
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;

public class MatchingCardController : MonoBehaviour
{
    #region  Public Variables

    public  Action<MatchingCardComponent> OnFlippedStartedEvent;
    public  Action<MatchingCardComponent> OnFlippedEndedEvent;
    public  Action<MatchingCardComponent> OnUnflippedStartedEvent;
    public  Action<MatchingCardComponent> OnUnflippedEndedEvent;
    public  Action<MatchingCardComponent> OnDissolveStartedEvent;
    public  Action<MatchingCardComponent, float> OnDissolvingEvent;
    public  Action<MatchingCardComponent> OnDissolvingCompletedEvent;

    #endregion

    #region Private Variables

    private static readonly int _hasKey_Shader_DissolveProgress = Shader.PropertyToID("_Dissolve");

    private Stack<MatchingCardComponent> _flippedCardsStack = new Stack<MatchingCardComponent>();

    #endregion

    #region Internal Callback

    private void OnFlippedStartedCallback(MatchingCardComponent value)
    {
        
    }

    private void OnFlippedEndedCallback(MatchingCardComponent value)
    {
        Debug.Log("Card Flipped Ended: " + value.CardMatchData.matchItemName, value.gameObject);
        if(_flippedCardsStack.Count > 0)
        {
            if(_flippedCardsStack.Peek().CardMatchData == value.CardMatchData)
            {
                value.TryDissolve();
                _flippedCardsStack.Pop().TryDissolve();
            }
            else
            {
                value.TryUnflip();
                while(_flippedCardsStack.Count > 0)
                {
                    _flippedCardsStack.Pop().TryUnflip();
                }
            }
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
        value.cardMeshRenderer.material.SetFloat(_hasKey_Shader_DissolveProgress, progression);
    }

    private void OnDissolvingCompletedCallback(MatchingCardComponent value)
    {
       
    }


    #endregion

    #region Private Variables

    private MatchData _currenMatchData = null;

    #endregion

    #region Unity Method

    private void OnEnable()
    {
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
