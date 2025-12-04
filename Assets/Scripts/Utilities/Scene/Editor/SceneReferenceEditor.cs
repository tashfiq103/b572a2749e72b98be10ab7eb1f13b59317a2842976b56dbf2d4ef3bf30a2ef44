#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        SerializedProperty scenePath = property.FindPropertyRelative("scenePath");
        SerializedProperty sceneName = property.FindPropertyRelative("sceneName");

        
        
        EditorGUI.BeginChangeCheck();

        SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath.stringValue);

        SceneAsset newScene = EditorGUI.ObjectField(
                        position,
                        "",
                        oldScene,
                        typeof(SceneAsset),
                        false
                    ) as SceneAsset;
        if (EditorGUI.EndChangeCheck())
        {

            string newPath = AssetDatabase.GetAssetPath(newScene);
            string[] splitedByDash = newPath.Split('/');
            string[] splitedByDot = splitedByDash[splitedByDash.Length - 1].Split('.');


            scenePath.stringValue = newPath;
            sceneName.stringValue = splitedByDot[0];

            property.serializedObject.ApplyModifiedProperties();
        }

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.EndProperty();
    }
}

#endif
