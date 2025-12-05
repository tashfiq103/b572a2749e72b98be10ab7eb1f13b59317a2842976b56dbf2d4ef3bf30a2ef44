using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    #region Public Variables

    public PhysicsRaycaster physicsRaycaster;

    #endregion

    #region Internal Callback

    private void OnLevelStartedCallback()
    {
        physicsRaycaster.enabled = true;
    }

    private void OnLevelEndedCallback()
    {
        physicsRaycaster.enabled = false;
    }

    #endregion

    #region Unity Callback

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStartedEvent.RegisterEvent(gameObject, OnLevelStartedCallback);
        GameManager.Instance.OnLevelEndedEvent.RegisterEvent(gameObject, OnLevelEndedCallback);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelStartedEvent.UnregisterEvent(gameObject);
        GameManager.Instance.OnLevelEndedEvent.UnregisterEvent(gameObject);
    }

    #endregion
}
