using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIButton : MonoBehaviour
{
     #region Unity Callback

    protected virtual void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonPressedCallback);
    }

    protected virtual void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonPressedCallback);
    }

    #endregion

    #region Abstract Method

    protected abstract void OnButtonPressedCallback();

    #endregion
}
