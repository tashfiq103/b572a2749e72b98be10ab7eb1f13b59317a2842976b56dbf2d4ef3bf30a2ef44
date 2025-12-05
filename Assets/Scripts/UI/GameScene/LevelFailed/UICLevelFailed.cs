using UnityEngine;

public class UICLevelFailed : UICanvas
{
   #region Internal Callback

    private void OnLevelFailedCallback()
    {
        Show();
    }

    #endregion

    #region Unity Callback

    private void OnEnable()
    {
        GameManager.Instance.OnLevelFailedEvent.RegisterEvent(gameObject, OnLevelFailedCallback);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelFailedEvent.UnregisterEvent(gameObject);
    }

    #endregion

    #region Abstract Method

    protected override void OnCanvasEnabled()
    {
        
    }

    protected override void OnCanvasDisabled()
    {
        
    }

    #endregion
}
