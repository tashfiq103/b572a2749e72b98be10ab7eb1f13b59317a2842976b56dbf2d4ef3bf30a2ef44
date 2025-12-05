using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Data/AudioData")]
public class AudioData : ScriptableObject
{
    #region Public Variables

    public float GetRandomVolume
    {
        get
        {
            float clampedMax = Mathf.Clamp(volumeMax, volumeMin, 1);
            return Random.Range(volumeMin, clampedMax);
        }
    }

    public float GetRandomPitch
    {
        get
        {
            float clampedMax = Mathf.Clamp(pitchMax, pitchMin, 3);
            return Random.Range(pitchMin, clampedMax);
        }
    }

    

    public AudioClip audioClip;

    [Space(20)]
    [Range(0f, 1)]
    public float volumeMin = 1f;
    [Range(0f, 1f)]
    public float volumeMax = 1;

    [Space(20)]
    [Range(-3, 3)]
    public float pitchMin = 1;
    [Range(-3, 3)]
    public float pitchMax = 1;

    #endregion
}
