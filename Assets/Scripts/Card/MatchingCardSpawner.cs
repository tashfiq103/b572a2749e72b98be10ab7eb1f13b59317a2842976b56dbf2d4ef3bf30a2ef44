using UnityEngine;

public class MatchingCardSpawner : MonoBehaviour
{

    #region Public Variables

    

    #endregion

    #region Internal Callback

    private void SpawnMatchingCards()
    {
        
    }

    #endregion

    #region Unity Callback

    private void OnEnable()
    {
        GameManager.Instance.OnLevelDataLoadedEvent.RegisterEvent(this.gameObject, SpawnMatchingCards);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelDataLoadedEvent.UnregisterEvent(this.gameObject);
    }

    #endregion   
}
