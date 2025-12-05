using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(GameEventData))]
public class GameEventDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameEventData gameEventData = (GameEventData)target;
        if(GUILayout.Button("Trigger Event"))
        {
            gameEventData.TriggerEvent();
        }
    }
}

#endif

[CreateAssetMenu(fileName = "GameEventData", menuName = "Data/GameEventData", order = 0)]
public class GameEventData : ScriptableObject
{
    #region Private Variables

     private Dictionary<GameObject, Action> _eventDictionary = new Dictionary<GameObject, Action>();

    #endregion

    #region Public Callback

    public void RegisterEvent(GameObject key, Action callback)
    {
        if(!_eventDictionary.ContainsKey(key))
        {
            _eventDictionary.Add(key, callback);
        }   
    }

    public void UnregisterEvent(GameObject key)
    {
        if(_eventDictionary.ContainsKey(key))
        {
            _eventDictionary.Remove(key);
        }   
    }

    public void TriggerEvent()
    {
        #if UNITY_EDITOR
        Debug.Log($"EventTriggered : {name}");
        #endif
        foreach(var pair in _eventDictionary)
        {
            pair.Value?.Invoke();
        }  
    }

    #endregion
   
}
