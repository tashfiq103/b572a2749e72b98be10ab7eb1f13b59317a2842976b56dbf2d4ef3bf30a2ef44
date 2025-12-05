using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Canvas))]
public abstract class UICanvas : MonoBehaviour
{
    #region Public Variables

    public GameObject rootObject;

    #endregion

    #region Unity Callback

    protected virtual void Awake()
    {
        GetComponent<Canvas>().enabled = rootObject.gameObject.activeInHierarchy;
    }

    #endregion

    #region Public Callback

    public void Show()
    {
        IEnumerator OneFrameDelay()
        {
            yield return null;
            OnCanvasEnabled();
        }
        GetComponent<Canvas>().enabled = true;
        rootObject.SetActive(true);
        StartCoroutine(OneFrameDelay());
    }

    public void Hide()
    {
        GetComponent<Canvas>().enabled = false;
        rootObject.SetActive(false);
    }

    #endregion

    #region Abstract Method

    protected abstract void OnCanvasEnabled();
    protected abstract void OnCanvasDisabled();

    #endregion
}
