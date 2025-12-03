using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventData", menuName = "Data/GameEventData", order = -1)]
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
        foreach(var pair in _eventDictionary)
        {
            pair.Value?.Invoke();
        }  
    }

    #endregion
   
}
