using Unity.VisualScripting;
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
        LevelData levelData = GameManager.Instance.LevelContainerDataReference.CurrentLevelDataReference;
        float rowPosition = -((levelData.Row - 1) * cardVerticalSpacing) / 2;
        for(int row = 0; row < levelData.Row; row++)
        {
            float columnPosition = -((levelData.Column - 1) * cardHorizontalSpacing) / 2;
            for(int column = 0; column < levelData.Column; column++)
            {
                
                Vector3 spawnPosition = new Vector3(
                    columnPosition,
                    0,
                    rowPosition
                );
                columnPosition += cardHorizontalSpacing;
                GameObject cardObject = Instantiate(cardPrefab, cardParentTransform);
                cardObject.transform.localPosition = spawnPosition;
                // Additional setup for the card can be done here using levelData.MatchDatas
            }
            rowPosition += cardVerticalSpacing;
        }
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
