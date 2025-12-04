using UnityEngine;

public class MatchingCardSpawner : MonoBehaviour
{

    #region Public Variables

    public GameObject cardPrefab;
    public Transform cardParentTransform;
    public float cardHorizontalSpacing = 2.0f;
    public float cardVerticalSpacing = 3.0f;

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

    private void Start()
    {
        //Debug Call For Loading Level Data and Start Leveling
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelDataLoadedEvent.UnregisterEvent(this.gameObject);
    }

    #endregion   
}
