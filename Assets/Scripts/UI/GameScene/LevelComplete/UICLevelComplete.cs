using UnityEngine;
using TMPro;

public class UICLevelComplete : UICanvas
{
    #region Public Variables

    public TextMeshProUGUI scoreText;

    #endregion

    #region Internal Callback

    private void OnLevelCompleteCallback()
    {
        scoreText.text = GameManager.Instance.matchingCardController.Score.ToString();
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
