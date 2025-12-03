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

            Debug.Log($"i = {i}, matchDataIndex = {matchDataIndex}");

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
            if(GUILayout.Button("Initialize Grid Data"))
            {
                InitializeGridData();
            }
        }
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

    [Range(2, 10)]
    [SerializeField] private int _row = 2;
    [Range(2, 10)]
    [SerializeField] private int _column = 2;

    [Space(25)]
    [SerializeField] private MatchData[] _matchDatas;





}
