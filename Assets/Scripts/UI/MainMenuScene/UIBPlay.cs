using UnityEngine;

public class UIBPlay : UIButton
{
    protected override void OnButtonPressedCallback()
    {
        DataManager.Instance.LevelDataContainerReference.LoadLevel();
    }
}
