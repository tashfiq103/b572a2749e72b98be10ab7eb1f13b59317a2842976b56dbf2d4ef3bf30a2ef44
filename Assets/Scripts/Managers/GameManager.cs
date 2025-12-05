using UnityEngine;

[DefaultExecutionOrder(-900)]
public class GameManager : MonoBehaviour
{
    #region Public Variables

    public static GameManager Instance{get; private set;}

    [Header("Managers")]
     public MatchingCardSpawner matchingCardSpawner;
    public MatchingCardController matchingCardController;
   

    [Header("Event")]
    public GameEventData OnLevelDataLoadedEvent;
    public GameEventData OnLevelStartedEvent;
    public GameEventData OnLevelEndedEvent;
    public GameEventData OnLevelCompletedEvent;
    public GameEventData OnLevelFailedEvent;

    #endregion

    #region Unity Callback

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    #endregion
}
