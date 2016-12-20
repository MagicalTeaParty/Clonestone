using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.Networking;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointEnter");
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
        //Debug.Log("OnPointerExit");
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

        #region AI
        //Wenn ich was auf der Gegner DropZone fallen lasse
        if(gameObject.name == "DropZoneP2Position")
        {
            //Debug.Log("DropZ2 " + eventData.pointerDrag.GetComponent<CardDataController>().Owner.GetComponent<NetworkIdentity>().isLocalPlayer);

            //Wenn die Karte eine Gegnerkarte ist
            if(eventData.pointerDrag.GetComponent<CardDataController>().Owner.GetComponent<NetworkIdentity>().isLocalPlayer == false)
            {
                Debug.Log("DropZ2Client");

                //setze den Status der Karte auf OnBoard
                eventData.pointerDrag.GetComponent<CardDataController>().Data.CardState = CardDataController.CardStatus.onBoard;
                
                //cardBackGameObjekt.SetActive(false);
                //cardLifeGameObjekt.SetActive(true);

            }
            else
            {
                //FUNKT NICHT - Fremde Karten müssen wieder zurück zum Ursprung
                //Dragable dd = eventData.pointerDrag.GetComponent<Dragable>();
                //if(dd!=null && dd.parentToReturnTo != null)
                //{
                //    dd.GetComponent<Transform>().SetParent(dd.parentToReturnTo);
                //}
            }
        }
        #endregion

        Dragable d = eventData.pointerDrag.GetComponent<Dragable>();
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }
        
    }
}
