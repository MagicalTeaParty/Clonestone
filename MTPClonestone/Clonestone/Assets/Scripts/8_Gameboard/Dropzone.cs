using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;



public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointEnter");
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
        {
            return;
        }

        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name);

        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }
    }
}
