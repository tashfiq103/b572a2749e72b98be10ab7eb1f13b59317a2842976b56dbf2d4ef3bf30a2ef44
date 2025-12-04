using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    #region  Private Variabls

    private LevelData _levelData;

    private SerializedProperty _matchDatas;

    private SerializedProperty _gridData;

    private Vector2 _scrollPosVertical;
    private Vector2 _scrollPosHorizontal;


    #endregion

    #region  Internal Method

    private void FillGridData(int gridIndex, MatchData matchData, ref List<int> gridIndices)
    {



        SerializedProperty _gridData_row = _gridData.GetArrayElementAtIndex(gridIndex).FindPropertyRelative("_row");
        _gridData_row.intValue = gridIndex / _levelData.Column;
        _gridData_row.serializedObject.ApplyModifiedProperties();


        SerializedProperty _gridData_column = _gridData.GetArrayElementAtIndex(gridIndex).FindPropertyRelative("_column");
        _gridData_column.intValue = gridIndex % _levelData.Column;
        _gridData_column.serializedObject.ApplyModifiedProperties();


        SerializedProperty _gridData_matchData = _gridData.GetArrayElementAtIndex(gridIndex).FindPropertyRelative("_matchData");
        _gridData_matchData.objectReferenceValue = matchData;
        _gridData_matchData.serializedObject.ApplyModifiedProperties();
        

        gridIndices.Remove(gridIndex);
    }

    private void InitializeGridData()
    {
        int totalGridCount = _levelData.Row * _levelData.Column;
        _gridData.arraySize = totalGridCount;
        _gridData.serializedObject.ApplyModifiedProperties();

        List<int> gridIndices = new List<int>();
        for(int i = 0; i < totalGridCount; i++)
            gridIndices.Add(i);
        
        for(int i = 0; i < totalGridCount / 2; i++)
        {

            
            int matchDataIndex = Random.Range(0, _matchDatas.arraySize);
            MatchData matchData = _matchDatas.GetArrayElementAtIndex(matchDataIndex).objectReferenceValue as MatchData;


            FillGridData(
                gridIndices[Random.Range(0, gridIndices.Count)],
                matchData,
                ref gridIndices
            );

            FillGridData(
                gridIndices[Random.Range(0, gridIndices.Count)],
                matchData,
                ref gridIndices
            );
        }
    }

    #endregion

    #region Untiy Methods

    private void OnEnable()
    {
        _levelData = (LevelData)target;

        if(_levelData == null)
            return; 

        _matchDatas = serializedObject.FindProperty("_matchDatas");

        _gridData = serializedObject.FindProperty("_gridDatas");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(!EditorApplication.isPlaying)
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("========================", EditorStyles.boldLabel);
            EditorGUILayout.Space(20);
            if(GUILayout.Button("Initialize Grid Data"))
            {
                InitializeGridData();
            }
        }

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("========================", EditorStyles.boldLabel);
        EditorGUILayout.Space(20);

        
        _scrollPosVertical = EditorGUILayout.BeginScrollView(_scrollPosVertical);
        {
            _scrollPosHorizontal = EditorGUILayout.BeginScrollView(_scrollPosHorizontal);
            {
                int totalGridCount = _levelData.Row * _levelData.Column;
                for(int row = 0; row < _levelData.Row; row++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        for(int column = 0; column < _levelData.Column; column++)
                        {
                            int gridIndex = row * _levelData.Column + column;

                            if(gridIndex >= _gridData.arraySize)
                            {
                               if(GUILayout.Button(Texture2D.redTexture, GUILayout.Width(60), GUILayout.Height(60)))
                                {
                                    Debug.Log($"Clicked on Empty Grid ({row}, {column})");
                                }
                            }
                            else
                            {
                                SerializedProperty gridDataElement = _gridData.GetArrayElementAtIndex(gridIndex);
                            SerializedProperty matchDataProperty = gridDataElement.FindPropertyRelative("_matchData");

                            if(matchDataProperty.objectReferenceValue == null)
                            {
                                if(GUILayout.Button(Texture2D.redTexture, GUILayout.Width(60), GUILayout.Height(60)))
                                {
                                    Debug.Log($"Clicked on Empty Grid ({row}, {column})");
                                }
                            }
                            else
                            {
                                MatchData matchData = matchDataProperty.objectReferenceValue as MatchData;
                                Texture2D texture = AssetPreview.GetAssetPreview(matchData.matchItemSprite);

                                if(GUILayout.Button(texture, GUILayout.Width(60), GUILayout.Height(60)))
                                {
                                    Debug.Log($"Clicked on Grid ({row}, {column}) with Match Item: {matchData.matchItemName}");
                                }
                            }
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndScrollView();
    }

    #endregion
}

#endif

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 3)]
public class LevelData : EnumData
{
    #region Custom DataType

    [System.Serializable]
    public class GridData
    {
        #region Public Variables

        public int Row{get => _row;}
        public int Column{get => _column;}
        public MatchData MatchDataReference{get => _matchData;}

        #endregion

        #region Privare Variables

        [SerializeField] private int _row;
        [SerializeField] private int _column;
        [SerializeField] private MatchData _matchData;

        #endregion
       
        #region Public Callback

        public void OverrideMatchData(MatchData value) => _matchData = value;

        #endregion
        
    }

    #endregion

    #region Public Variables

    public int Row{get => _row;}
    public int Column{get => _column;}
    public MatchData[] MatchDatas{get => _matchDatas;}

    #endregion

    [Header("=== LevelData ===")]

    [SerializeField, HideInInspector] private GridData[] _gridDatas;

    public SceneReference levelScene;

    [Space(25)]
    [Range(2, 15)]
    [SerializeField] private int _row = 2;
    [Range(2, 15)]
    [SerializeField] private int _column = 2;

    [Space(25)]
    [SerializeField] private MatchData[] _matchDatas;


    


}
