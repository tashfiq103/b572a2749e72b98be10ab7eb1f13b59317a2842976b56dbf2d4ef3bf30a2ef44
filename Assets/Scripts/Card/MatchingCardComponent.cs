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

    #endregion

    #region Internal Callback

    private void ChangeCardState(CardStates newState)
    {
        CardState = newState;
        OnCardStateChangedEvent?.Invoke(CardState);
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
                //Flip to Back
                break;
        }
    }

    #endregion

    
}
