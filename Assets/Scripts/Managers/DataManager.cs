using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class DataManager : MonoBehaviour
{
    #region Public Variables

    public static DataManager Instance{get; private set;}

    public LevelDataContainer LevelDataContainerReference{get => _levelContainerData;}

    #endregion

    #region Private Variables

    [Header("Data")]
    [SerializeField] private LevelDataContainer _levelContainerData;

    #endregion

    #region Unity Callback

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnGameStartCallback()
    {
        DataManager dataManager = Instantiate(
            Resources.Load<DataManager>("DataManager").gameObject
        ).GetComponent<DataManager>();

        dataManager.name = "DataManager";
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);    
        }
    }

    #endregion
}
