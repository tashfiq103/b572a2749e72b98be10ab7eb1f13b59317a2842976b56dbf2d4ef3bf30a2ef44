using UnityEngine;
using TMPro;

public class UICLevelFailed : UICanvas
{
    #region Public Variables

    public TextMeshProUGUI scoreText;

    #endregion


   #region Internal Callback

    private void OnLevelFailedCallback()
    {
        scoreText.text = GameManager.Instance.matchingCardController.Score.ToString();
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
