using UnityEngine;

public class UICLevelComplete : UICanvas
{
    #region Internal Callback

    private void OnLevelCompleteCallback()
    {
        Show();
    }

    #endregion

    #region Unity Callback

    private void OnEnable()
    {
        GameManager.Instance.OnLevelCompletedEvent.RegisterEvent(gameObject, OnLevelCompleteCallback);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelCompletedEvent.UnregisterEvent(gameObject);
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
