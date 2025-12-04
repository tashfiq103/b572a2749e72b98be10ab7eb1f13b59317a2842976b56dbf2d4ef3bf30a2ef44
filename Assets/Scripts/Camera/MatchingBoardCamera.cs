
using Cinemachine;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class MatchingBoardCamera : MonoBehaviour
{
    #region Public Variables

    [Range(0f, 2f)]
    public float additionalPaddingOnOrthographicSize = 1.0f;

    #endregion

    #region Internal Function

    public static Vector2 GetEditorGameViewSize()
    {
        #if UNITY_EDITOR
            return Handles.GetMainGameViewSize();
        #else
            return new Vector2(Screen.width, Screen.height);
        #endif
    }

    #endregion

    #region  Internal Callback

    private void OnDataLoadedCallback()
    {
        float orthoSize = 0;

        int rows = GameManager.Instance.LevelContainerDataReference.CurrentLevelDataReference.Row;
        int columns = GameManager.Instance.LevelContainerDataReference.CurrentLevelDataReference.Column;
        
        float cellSizeX = GameManager.Instance.matchingCardSpawner.cardHorizontalSpacing;
        float cellSizeY = GameManager.Instance.matchingCardSpawner.cardVerticalSpacing;


        /*
        When it is about to overlow, calculating how much of orthograpgic size would be taken to fit the whole grid in the view
        and adding that to the orthographic size. For the landscape mode, if the columns are more than rows, you would start seeing spaces are coming on top-bottom due to aspect ratino,
        vice versa for portrait mode.
        */

        Vector2 screenSize   = GetEditorGameViewSize();

        bool isPortrait     = screenSize.y >= screenSize.x ? true : false;
        float aspectRatio= (float) Mathf.Max(screenSize.x, screenSize.y) / Mathf.Min(screenSize.x, screenSize.y);
        
        if(isPortrait)
        {
            //Portrait
            float gridWidth           = columns * cellSizeX;
            float gridHeight          = gridWidth * aspectRatio;

            int rowThreshold = Mathf.FloorToInt(gridHeight / cellSizeY);
            if(rows > columns)
            {
                if(rows > rowThreshold)
                {
                    orthoSize += (rows - rowThreshold) * (1f / rowThreshold) * (gridHeight / 2f);
                }
                orthoSize += gridHeight / 2f;
            }
            else
            {
                orthoSize = gridHeight / 2f;
            }
        }
        else
        {
            //Landscape

            float gridHeight          = rows * cellSizeY;
            float gridWidth           = gridHeight * aspectRatio;

            if(columns > rows)
            {
                
                int columnThreshold = Mathf.FloorToInt(gridWidth / cellSizeX);
                if(columns > columnThreshold)
                {
                    orthoSize += (columns - columnThreshold) * (1f / columnThreshold) * (gridHeight / 2f);
                }
               
                orthoSize += gridHeight / 2f;
            }
            else
            {
                orthoSize = gridHeight / 2f;
            }
        }


        gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = orthoSize + additionalPaddingOnOrthographicSize;

        
    }

    #endregion

    #region Unity Callback


    private void OnEnable()
    {
        GameManager.Instance.OnLevelDataLoadedEvent.RegisterEvent(this.gameObject, OnDataLoadedCallback);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelDataLoadedEvent.UnregisterEvent(this.gameObject);
    }

    #endregion
}
