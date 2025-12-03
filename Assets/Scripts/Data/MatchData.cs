using UnityEngine;

[CreateAssetMenu(fileName = "MatchData", menuName = "Data/MatchData", order = 2)]
public class MatchData : ScriptableObject
{
    [Header("=== MatchData ===")]
    public string matchItemName;
    public Sprite matchItemSprite;
}
