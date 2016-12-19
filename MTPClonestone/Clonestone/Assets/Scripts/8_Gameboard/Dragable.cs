using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

    GameObject placeholder = null;




    public void OnBeginDrag(PointerEventData eventData)
    {
        zDisplacement = -Camera.main.transform.position.z + transform.position.z;

        //Debug.Log("BeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent.parent);
        LayoutElement lay = placeholder.AddComponent<LayoutElement>();
        lay.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        lay.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        lay.flexibleWidth = 0;
        lay.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    private float zDisplacement;
    // returns mouse position in World coordinates for our GameObject to follow. 
    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        //Debug.Log(screenMousePos);
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        this.transform.position = MouseInWorldCoords();// eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)

            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("EndDrag");

        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);

        //Hier wird das Dragable-Script deaktiviert, sobald die Karte "onBoard" ist
        if (GameboardInitController.Players[0].GetComponent<PlayerDataController>().Data.IsActivePLayer && GetComponent<Transform>().parent == GameObject.FindGameObjectWithTag("DropZone1").GetComponent<Transform>())
        {
            GetComponent<Dragable>().enabled = false;
            gameObject.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.onBoard;

            GameboardInitController.Players[0].GetComponent<PlayerDataController>().Data.CurrentActiveMana -= gameObject.GetComponent<CardDataController>().Data.Mana;
        }
        else if (GameboardInitController.Players[1].GetComponent<PlayerDataController>().Data.IsActivePLayer && GetComponent<Transform>().parent == GameObject.FindGameObjectWithTag("DropZone2").GetComponent<Transform>())
        {
            GetComponent<Dragable>().enabled = false;
            gameObject.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.onBoard;

            GameboardInitController.Players[1].GetComponent<PlayerDataController>().Data.CurrentActiveMana -= gameObject.GetComponent<CardDataController>().Data.Mana;
        }

        //Aktiviere das Attack-Script
        //setze z-index
        //this.GetComponent<RectTransform>().position.z = -9216;

            //Draggable a = GameObject.Find("Target")
            //a.enabled = true;
            //DragCreatureAttack at = this.GetComponentInChildren<DragCreatureAttack>();
            //at.enabled = true;
    }

}
