using UnityEngine;

public class UIBNextLevel : UIButton
{
    protected override void OnButtonPressedCallback()
    {
        DataManager.Instance.LevelDataContainerReference.LoadNextLevel();
    }
}
