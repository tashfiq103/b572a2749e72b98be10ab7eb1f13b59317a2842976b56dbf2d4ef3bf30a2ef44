using TMPro;
public class UICScoreHUD : UICanvas
{
    #region Public Variables

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    #endregion

    #region Internal Callback

    private void OnScoreUpdatedCallback(int value)
    {
        scoreText.text = $"Score {value:00}";
    }

    private void OnComboStackUpdatedCallback(int value)
    {
         comboText.text = $"Combo x{value:0}";
    }

    private void OnLevelStartedCallback()
    {
        GameManager.Instance.matchingCardController.OnScoreUpdatedEvent         += OnScoreUpdatedCallback;
        GameManager.Instance.matchingCardController.OnComboStackUpdatedEvent    += OnComboStackUpdatedCallback;
        Show();
    }

    private void OnLevelEndedCallback()
    {
        GameManager.Instance.matchingCardController.OnScoreUpdatedEvent         -= OnScoreUpdatedCallback;
        GameManager.Instance.matchingCardController.OnComboStackUpdatedEvent    -= OnComboStackUpdatedCallback;
        Hide();
    }

    #endregion

    #region Unity Method

    private void OnEnable()
    {
        GameManager.Instance.OnLevelStartedEvent.               RegisterEvent(gameObject, OnLevelStartedCallback);
        GameManager.Instance.OnLevelEndedEvent.RegisterEvent(gameObject, OnLevelEndedCallback);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelStartedEvent.UnregisterEvent(gameObject);
        GameManager.Instance.OnLevelEndedEvent.UnregisterEvent(gameObject);
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
