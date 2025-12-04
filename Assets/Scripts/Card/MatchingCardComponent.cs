using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
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
    public MatchData CardMatchData{get; private set;}


    public event Action<CardStates> OnCardStateChangedEvent;

    public Transform meshContainerTransform;
    public MeshRenderer cardMeshRenderer;
    public Animator cardAnimator;

    #endregion

    #region Private Variabls


    private static readonly int _hashKey_Animation_Flip = Animator.StringToHash("Flip");
    private static readonly int _hashKey_Animation_Unflip = Animator.StringToHash("Unflip");
    private static readonly int _hashKey_Animation_Dissolve = Animator.StringToHash("Dissolve");


    private static readonly int _hashKey_Shader_BaseMap = Shader.PropertyToID("_BaseMap");
    private static readonly int _hasKey_Shader_DissolveProgress = Shader.PropertyToID("_Dissolve");

    #endregion

    #region Internal Callback

    private void ChangeCardState(CardStates newState)
    {
        CardState = newState;
        switch(CardState)
        {
            case CardStates.BackFaced:
                //Handle Back Faced State
                cardAnimator.SetTrigger(_hashKey_Animation_Unflip);
                break;

            case CardStates.FrontFaced:
                //Handle Front Faced State
                cardAnimator.SetTrigger(_hashKey_Animation_Flip);
                break;

            case CardStates.Dissolving:
                //Handle Dissolving State
                cardAnimator.SetTrigger(_hashKey_Animation_Dissolve);
                break;

            case CardStates.Matched:
                //Handle Matched State
                Destroy(gameObject);
                break;
        }
        OnCardStateChangedEvent?.Invoke(CardState);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch(CardState)
        {
            case CardStates.BackFaced:
                ChangeCardState(CardStates.FrontFaced);
                break;

            case CardStates.FrontFaced:
                ChangeCardState(CardStates.BackFaced);
                break;
        }
    }

    #endregion

    #region Unity Method

    private void Awake()
    {
        
    }

    #endregion

    #region Public Callback

    public void Initialize(MatchData cardMatchData)
    {
        CardMatchData = cardMatchData;

        cardMeshRenderer.material.SetTexture(
            _hashKey_Shader_BaseMap,
            CardMatchData.matchItemSprite.texture
        );
    }
    
    

    public bool TryDissolve(Action<MatchingCardComponent, float> OnDissolving, Action<MatchingCardComponent> OnDissolveComplete)
    {
        if(CardState == CardStates.FrontFaced)
        {
            ChangeCardState(CardStates.Dissolving);

            IEnumerator DissolveProgress()
            {
                yield return null;
                yield return null;
                yield return null;

                AnimatorStateInfo info  = cardAnimator.GetCurrentAnimatorStateInfo(0);
                float duration          = info.length / info.speed;
                float remainingTime     = duration;
                while(remainingTime > 0)
                {
                    float deltaTime = Time.deltaTime;
                    remainingTime -= deltaTime;
                    remainingTime = Mathf.Clamp(remainingTime, 0, duration);

                    float progress = 1 - (remainingTime / duration);
                    cardMeshRenderer.material.SetFloat(_hasKey_Shader_DissolveProgress, progress);


                    OnDissolving?.Invoke(this, progress);

                    yield return new WaitForSeconds(deltaTime);
                }

                OnDissolveComplete?.Invoke(this);
                ChangeCardState(CardStates.Matched);
            }

            StartCoroutine(DissolveProgress());

            return true;
        }

        return false;
    }

    public bool TryUnflip()
    {
        if(CardState == CardStates.FrontFaced)
        {
            ChangeCardState(CardStates.BackFaced);
            return true;
        }

        return false;
    }

    #endregion

    
}
