using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SoundFXFeedback))]
public class SoundFXPlayerEditor : Editor
{
    #region Variables

    private SoundFXFeedback _reference;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        _reference = (SoundFXFeedback) target;
        if(_reference == null)
            return;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        if(_reference.audioData != null
        && _reference.audioData.audioClip != null)
        {
            EditorGUILayout.LabelField("=============");
            if(GUILayout.Button("PlaySound"))
            {
                if(_reference.TryPlaySound())
                {
                    
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}
#endif

[RequireComponent(typeof(AudioSource))]
[ExecuteAlways]
public class SoundFXFeedback : MonoBehaviour
{
    #region Public Variables

    public AudioData audioData;

    #endregion

    #region Private Variables

    private AudioSource _audioSource;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.Stop();
    }


    #endregion

    #region Public Callback

    public bool TryPlaySound(bool resetIfPlaying = true)
    {
        if(_audioSource.isPlaying && !resetIfPlaying)
            return false;

        _audioSource.Stop();

        _audioSource.clip   = audioData.audioClip;
        _audioSource.volume = audioData.GetRandomVolume;
        _audioSource.pitch  = audioData.GetRandomPitch;

        _audioSource.Play();

        return true;
    }

    #endregion
}
