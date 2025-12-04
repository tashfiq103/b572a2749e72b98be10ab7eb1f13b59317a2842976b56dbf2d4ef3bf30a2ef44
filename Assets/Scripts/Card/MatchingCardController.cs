
using UnityEngine;
using System;
using System.Runtime.InteropServices;

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

    #region Internal Callback

    private void OnFlippedStartedCallback(MatchingCardComponent value)
    {
        
    }

    private void OnFlippedEndedCallback(MatchingCardComponent value)
    {
        
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
