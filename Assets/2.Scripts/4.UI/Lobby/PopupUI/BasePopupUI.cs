using EtudeProject;
using UnityEngine;

public class BasePopupUI : MonoBehaviour
{
    public UIType type;

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
