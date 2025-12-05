using UnityEngine;

public class UIBRetryLevel : UIButton
{
    protected override void OnButtonPressedCallback()
    {
        DataManager.Instance.LevelDataContainerReference.LoadLevel();
    }
}
