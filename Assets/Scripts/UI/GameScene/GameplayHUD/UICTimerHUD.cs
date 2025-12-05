
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICTimerHUD : UICanvas
{
    #region Public Variabls

    public TextMeshProUGUI timeText;
    public Image timeClockImage;

    #endregion

    #region Internal Callback

    private void OnTimerUpdatedCallback(float reminder, float duration)
    {
        int minutes = Mathf.FloorToInt(reminder / 60f);
        int seconds = Mathf.FloorToInt(reminder % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";

        timeClockImage.fillAmount = reminder / duration;
    }


    private void OnLevelStartedCallback()
    {
        GameManager.Instance.matchingCardController.OnTimerUpdatedEvent         += OnTimerUpdatedCallback;
        Show();
    }

    private void OnLevelEndedCallback()
    {
        GameManager.Instance.matchingCardController.OnTimerUpdatedEvent         -= OnTimerUpdatedCallback;
        Hide();
    }

    #endregion

    #region Unity Method

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

    #region Abstract Method

    protected override void OnCanvasEnabled()
    {
        
    }

    protected override void OnCanvasDisabled()
    {
        
    }

    #endregion
}
