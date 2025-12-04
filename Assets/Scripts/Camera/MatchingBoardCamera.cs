
using Cinemachine;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class MatchingBoardCamera : MonoBehaviour
{
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

        float gridWidth  = columns * cellSizeX;
        float gridHeight = rows * cellSizeY;

        Vector2 screenSize   = GetEditorGameViewSize();

        bool isPortrait     = screenSize.y >= screenSize.x ? true : false;
        float aspectRatioMax= (float) Mathf.Max(screenSize.x, screenSize.y) / Mathf.Min(screenSize.x, screenSize.y);
        float aspectRatioMin= (float) Mathf.Min(screenSize.x, screenSize.y) / Mathf.Max(screenSize.x, screenSize.y);
        Debug.Log($"Screen Width : {screenSize.x} | Screen Height : {screenSize.y} | Aspect Ratio : {aspectRatioMax} | isPortrait : {isPortrait} | Grid Width : {gridWidth} | Grid Height : {gridHeight}");
        if(isPortrait)
        {
            //Portrait
            gridWidth           = columns * cellSizeX;
            gridHeight          = gridWidth * aspectRatioMax;

            int rowThreshold = Mathf.FloorToInt(gridHeight / cellSizeY);
            Debug.Log($"Portrait Mode - Row Threshold : {rowThreshold} | Rows : {rows} | gridWidth : {gridWidth} | gridHeight : {gridHeight}");
            if(rows > columns)
            {
                if(rows <= rowThreshold)
                {
                    
                }
                else
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

            gridHeight          = rows * cellSizeY;
            gridWidth           = gridHeight * aspectRatioMax;

            if(columns > rows)
            {
                
                int columnThreshold = Mathf.FloorToInt(gridWidth / cellSizeX);
                Debug.Log($"Landscape Mode - Column Threshold : {columnThreshold} | Columns : {columns} | gridWidth : {gridWidth} | gridHeight : {gridHeight}");
                if(columns <= columnThreshold)
                {
                }
                else
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


        gameObject.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = orthoSize;

        
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
