using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SceneReference
{
    public string scenePath;
    public string sceneName;



    public static implicit operator string(SceneReference sceneReference)
    {
        return sceneReference.sceneName;
    }

    
}
