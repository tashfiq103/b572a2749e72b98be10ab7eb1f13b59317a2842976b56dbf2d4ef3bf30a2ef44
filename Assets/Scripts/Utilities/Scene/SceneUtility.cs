using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public static class SceneUtility
{
    public static void LoadScene(
        this SceneReference sceneReference,
        UnityAction<float> OnUpdatingProgression = null,
        UnityAction OnSceneLoaded = null,
        float animationSpeedForLoadingBar = 1,
        float initalDelayToInvokeOnSceneLoaded = 0,
        LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        SceneTransitionController.LoadScene(
            sceneReference.sceneName,
            OnUpdatingProgression,
            OnSceneLoaded,
            animationSpeedForLoadingBar,
            initalDelayToInvokeOnSceneLoaded,
            loadSceneMode);
    }
}
