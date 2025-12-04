using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchingCardComponent : MonoBehaviour, IPointerClickHandler
{

    #region Custom Variabls

    public enum CardStates
    {
        BackFaced,
        FrontFaced,
        Dissolving,
        Matched
    }

    #endregion

    #region  Public Variables

    public CardStates CardState{get; private set;} = CardStates.BackFaced;
    public event Action<CardStates> OnCardStateChangedEvent;

    public Transform meshContainerTransform;
    public Animator cardAnimator;

    #endregion

    #region Private Variabls


    private static int _hasKey_Animation_Flip = Animator.StringToHash("Flip");
    private static int _hasKey_Animation_Unflip = Animator.StringToHash("Unflip");
    private static int _hasKey_Animation_Dissolve = Animator.StringToHash("Dissolve");

    #endregion

    #region Internal Callback

    private void ChangeCardState(CardStates newState)
    {
        CardState = newState;
        OnCardStateChangedEvent?.Invoke(CardState);
    }

    #endregion

    #region Unity Method

    private void Awake()
    {
        
    }

    #endregion

    #region Public Callback

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(CardState)
        {
            case CardStates.BackFaced:
                //Flip to Front
                ChangeCardState(CardStates.FrontFaced);
                cardAnimator.SetTrigger(_hasKey_Animation_Flip);
                Debug.Log("Card Flipped to Front", gameObject);
                break;

            case CardStates.FrontFaced:
                ChangeCardState(CardStates.BackFaced);
                cardAnimator.SetTrigger(_hasKey_Animation_Unflip);
                //Flip to Back
                break;

            case CardStates.Dissolving:
            
                cardAnimator.SetTrigger(_hasKey_Animation_Dissolve);
                Debug.Log("Card Dissolving", gameObject);
            break;
        }
    }

    public void Dissolve()
    {
        if(CardState == CardStates.FrontFaced)
        {
            ChangeCardState(CardStates.Dissolving);
            
        }
    }

    #endregion

    
}
