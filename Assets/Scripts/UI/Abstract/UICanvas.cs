using System.Collections;
using UnityEngine;

public abstract class UICanvas : MonoBehaviour
{
    #region Public Variables

    public GameObject rootObject;

    #endregion

    #region Public Callback

    public void Show()
    {
        IEnumerator OneFrameDelay()
        {
            yield return null;
            OnCanvasEnabled();
        }

        rootObject.SetActive(true);
        StartCoroutine(OneFrameDelay());
    }

    public void Hide()
    {
        rootObject.SetActive(false);
    }

    #endregion

    #region Abstract Method

    protected abstract void OnCanvasEnabled();
    protected abstract void OnCanvasDisabled();

    #endregion
}
