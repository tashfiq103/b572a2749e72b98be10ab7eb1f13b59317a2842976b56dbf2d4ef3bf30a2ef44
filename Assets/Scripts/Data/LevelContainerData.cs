using UnityEngine;

[CreateAssetMenu(fileName = "LevelContainerData", menuName = "Data/LevelContainerData", order = 2)]
public class LevelContainerData : ScriptableObject
{
    #region Public Variables

    public int LevelIndex
    {
        get
        {
            return PlayerPrefs.GetInt(LEVEL_INDEX_KEY, 0);
        }
    }

    public LevelData CurrentLevelDataReference
    {
        get
        {
            if(levelDatas.Length > 0)
            {
                int levelIndex = Mathf.Clamp(LevelIndex, 0, levelDatas.Length - 1);
                return levelDatas[levelIndex];
            }
            
            return null;
        }
    }

    public LevelData[] levelDatas;

    #endregion

    #region Private Variabls

    private const string LEVEL_INDEX_KEY = "LEVEL_INDEX_KEY";

    #endregion

    #region Unity Calback

    
    #endregion

    #region  Public Callback

    public void LoadLevel()
    {
        levelDatas[LevelIndex].levelScene.LoadScene(
            initalDelayToInvokeOnSceneLoaded : .1f,
            OnSceneLoaded: () =>
            {
                GameManager.Instance.OnLevelDataLoadedEvent.TriggerEvent();
                GameManager.Instance.OnLevelStartedEvent.TriggerEvent();
            }
        );
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = LevelIndex + 1;
        if (nextLevelIndex >= levelDatas.Length)
            nextLevelIndex = 0;

        PlayerPrefs.SetInt(LEVEL_INDEX_KEY, nextLevelIndex);

        LoadLevel();
    }

    public void RestartLevel()
    {
        LoadLevel();
    }


    #endregion
    
}
