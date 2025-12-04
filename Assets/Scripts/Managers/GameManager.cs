using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class GameManager : MonoBehaviour
{
    #region Private Variables

    [Header("Data")]
    [SerializeField] private LevelContainerData _levelContainerData;

    #endregion

    #region Public Variables

    public static GameManager Instance{get; private set;}

    public LevelContainerData LevelContainerDataReference{get => _levelContainerData;}
    
    [Header("Managers")]
    public MatchingCardSpawner matchingCardSpawner;

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
