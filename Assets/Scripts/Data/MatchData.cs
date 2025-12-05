using UnityEngine;

[CreateAssetMenu(fileName = "MatchData", menuName = "Data/MatchData", order = 4)]
public class MatchData : ScriptableObject
{
    [Header("=== MatchData ===")]
    public string matchItemName;
    public Sprite matchItemSprite;

    [Space(20)]
    [Range(0f, 1f)]
    public float iconSize = .8f;

    [Space(20)]
    public Color frontColorBottom;
    public Color frontColorTop;

    [Space(20)]
    [ColorUsage(true, true)]
    public Color dissolveColor;
}
