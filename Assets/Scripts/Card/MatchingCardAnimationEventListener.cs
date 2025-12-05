using System.Collections;
using UnityEngine;

public class MatchingCardAnimationEventListener : MonoBehaviour
{
    #region Public Variables

    public MatchingCardComponent matchingCardComponent;

    #endregion

    #region Private Variables

    private MatchingCardController _matchingCardController;

    #endregion

    #region Unity Method

    private void Awake()
    {
        _matchingCardController = GameManager.Instance.matchingCardController;
    }

    #endregion

    #region Public Method

    public void OnFlipAnimationStart()
    {
        _matchingCardController.OnFlippedStartedEvent?.Invoke(matchingCardComponent);
    }
    public void OnFlipAnimationComplete()
    {
        _matchingCardController.OnFlippedEndedEvent?.Invoke(matchingCardComponent);
    }

    public void OnUnflipAnimationStart()
    {
        _matchingCardController.OnUnflippedStartedEvent?.Invoke(matchingCardComponent);
    }

    public void OnUnflipAnimationComplete()
    {
        _matchingCardController.OnUnflippedEndedEvent?.Invoke(matchingCardComponent);

        if(!GameManager.Instance.IsGameRunning
        && matchingCardComponent.CardMatchIndex == DataManager.Instance.LevelDataContainerReference.CurrentLevelDataReference.GridDatas.Length -1)
        {
            GameManager.Instance.OnLevelStartedEvent.TriggerEvent();
        }
    }

    public void OnDissolveAnimationStart()
    {
        _matchingCardController.OnDissolveStartedEvent?.Invoke(matchingCardComponent);
    }

    public void OnDissolveAnimationComplete()
    {
        
    }

    #endregion
}
