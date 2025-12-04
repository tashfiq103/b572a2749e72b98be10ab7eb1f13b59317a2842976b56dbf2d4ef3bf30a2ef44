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
    }

    public void OnDissolveAnimationStart()
    {
        _matchingCardController.OnDissolveStartedEvent?.Invoke(matchingCardComponent);

        IEnumerator DissolveProgress()
        {
            yield return null;

            AnimatorStateInfo info  = matchingCardComponent.cardAnimator.GetCurrentAnimatorStateInfo(0);
            float duration          = info.length / info.speed;
            float remainingTime     = duration;
            while(remainingTime > 0)
            {
                float deltaTime = Time.deltaTime;
                remainingTime -= deltaTime;
                remainingTime = Mathf.Clamp(remainingTime, 0, duration);

                float progress = 1 - (remainingTime / duration);
                _matchingCardController.OnDissolvingEvent?.Invoke(matchingCardComponent, progress);

                yield return new WaitForSeconds(deltaTime);
            }
            
        }

        StartCoroutine(DissolveProgress());
    }

    public void OnDissolveAnimationComplete()
    {
        _matchingCardController.OnDissolvingCompletedEvent?.Invoke(matchingCardComponent);
    }

    #endregion
}
