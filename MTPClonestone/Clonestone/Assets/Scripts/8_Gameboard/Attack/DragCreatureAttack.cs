﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragCreatureAttack : DraggingActions
{

    // reference to the sprite with a round "Target" graphic
    private SpriteRenderer sr;
    // LineRenderer that is attached to a child game object to draw the arrow
    private LineRenderer lr;
    //// reference to WhereIsTheCardOrCreature to track this object`s state in the game
    //private WhereIsTheCardOrCreature whereIsThisCreature;
    // the pointy end of the arrow, should be called "Triangle" in the Hierarchy
    private Transform triangle;
    // SpriteRenderer of triangle. We need this to disable the pointy end if the target is too close.
    private SpriteRenderer triangleSR;
    // when we stop dragging, the gameObject that we were targeting will be stored in this variable.
    private GameObject Target;
    //// Reference to creature manager, attached to the parent game object
    //private OneCreatureManager manager;

    GameObject info;

    void Awake()
    {
        // establish all the connections
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponentInChildren<LineRenderer>();
        //lr.sortingLayerName = "AboveEverything";
        triangle = transform.Find("Triangle");
        triangleSR = triangle.GetComponent<SpriteRenderer>();

        //manager = GetComponentInParent<OneCreatureManager>();
        //whereIsThisCreature = GetComponentInParent<WhereIsTheCardOrCreature>();

        info = GameObject.Find("/Board/Info");
    }

    public override bool CanDrag
    {
        get
        {
            //TEST LINE: just for testing
             return true;

            //we can drag this card if

            //a) we can control this our player (this is checked in base.canDrag)
            // b) creature "CanAttackNow" - this info comes from logic part of our code into each creature`s manager script
            //return base.CanDrag && manager.CanAttackNow;
        }
    }

    public override void OnStartDrag()
    {
        //whereIsThisCreature.VisualState = VisualStates.Dragging;
        // enable target graphic
        sr.enabled = true;
        // enable line renderer to start drawing the line.
        lr.enabled = true;
    }

    public override void OnDraggingInUpdate()
    {
        Vector3 notNormalized = transform.position - transform.parent.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction * 2.3f).magnitude;
        if (notNormalized.magnitude > distanceToTarget)
        {
            // draw a line between the creature and the target
            lr.SetPositions(new Vector3[] { transform.parent.position, transform.position - direction * 2.3f });
            lr.enabled = true;

            // position the end of the arrow between near the target.
            triangleSR.enabled = true;
            triangleSR.transform.position = transform.position - 1.5f * direction;

            // proper rotarion of arrow end
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            triangleSR.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            // if the target is not far enough from creature, do not show the arrow
            lr.enabled = false;
            triangleSR.enabled = false;
        }


        RaycastHit[] hits;
        // TODO: raycast here anyway, store the results in 
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
            direction: (-Camera.main.transform.position + this.transform.position).normalized,
            maxDistance: 30f);

        CardDataController myData = this.GetComponentInParent<CardDataController>();

        foreach (RaycastHit h in hits)
        {
            CardDataController rayData = h.transform.gameObject.GetComponentInParent<CardDataController>();
            if (myData != rayData)
            {
                if (myData.Owner != rayData.Owner && myData != null && rayData != null)
                {
                    if (rayData.Data.Health - myData.Data.Attack <= 0)
                        rayData.gameObject.transform.Find("Canvas/CardPanel").GetComponent<Image>().color = new Color(1, 0, 0, 0.8f);
                    else if (myData.Data.Attack > 0)
                        rayData.gameObject.transform.Find("Canvas/CardPanel").GetComponent<Image>().color = new Color(1, 1, 0, 0.8f);
                    if (myData.Data.Health - rayData.Data.Attack <= 0)
                        myData.gameObject.transform.Find("Canvas/CardPanel").GetComponent<Image>().color = new Color(1, 0, 0, 0.8f);
                    else if (rayData.Data.Attack > 0)
                        myData.gameObject.transform.Find("Canvas/CardPanel").GetComponent<Image>().color = new Color(1, 1, 0, 0.8f);
                }
            }

        }
    }

    /// <summary>
    /// Prüft ob es eine Fremdkarte ist, wenn ja wird ein Angriff am Brett durchgeführt.
    /// </summary>
    public override void OnEndDrag()
    {
        Target = null;
        RaycastHit[] hits;
        // TODO: raycast here anyway, store the results in 
        hits = Physics.RaycastAll(origin: Camera.main.transform.position,
            direction: (-Camera.main.transform.position + this.transform.position).normalized,
            maxDistance: 30f);

        CardDataController myData = this.GetComponentInParent<CardDataController>();

        foreach(RaycastHit h in hits)
        {
            CardDataController rayData = h.transform.gameObject.GetComponentInParent<CardDataController>();
            Debug.Log(rayData.ToString());

            //Die Karten müssen unterschiedlich sein
            //Die Besitzer der Karten müssen ray unterschiedlich sein
            //&& rayData.Data.CardState == CardDataController.CardStatus.onBoard //Die Karte muss am Brett sein -- GEHT NOCH NICHT

            //Die aktuelle Karte muss sich von der geraycasteten Unterscheiden!
            if (myData != rayData)
            {
                if(myData.Owner != rayData.Owner && myData != null && rayData != null)
                {
                    Debug.Log(myData.Data.CardName + " (" + myData.Data.Attack + ") hits " + rayData.Data.CardName + "(" + rayData.Data.Health + ")");

                    if (rayData.Data.TypeName == "Hero")
                        rayData.Owner.GetComponent<PlayerDataController>().Data.CurrentHealth -= myData.Data.Attack;
                    else
                        //Die fremde Karte bekommt den Angriff ab
                        rayData.Data.Health -= myData.Data.Attack;
                    //Die Angriffskarte bekommt die Verteidigung ab
                    myData.Data.Health -= rayData.Data.Attack;

                    Debug.Log(rayData.Data.CardName + "(" + rayData.Data.Health + ")");

                    ShowAttackInfo(myData);

                    myData.Data.hasAttacked = true;
                }
            }

            //Debug.Log("Ray: " + h.transform.tag);
            //Debug.Log("Ray: " + h.transform.gameObject.tag);
            //Debug.Log("Ray: " + h.transform.gameObject.GetComponentInParent<CardDataController>().Data.CardName);
        }

        //foreach (RaycastHit h in hits)
        //{
        //    if ((h.transform.tag == "TopPlayer" && this.tag == "LowCreature") ||
        //        (h.transform.tag == "LowPlayer" && this.tag == "TopCreature"))
        //    {
        //        // go face
        //        Target = h.transform.gameObject;
        //    }
        //    else if ((h.transform.tag == "TopCreature" && this.tag == "LowCreature") ||
        //            (h.transform.tag == "LowCreature" && this.tag == "TopCreature"))
        //    {
        //        // hit a creature, save parent transform
        //        Target = h.transform.parent.gameObject;
        //    }

        //}

        bool targetValid = false;

        //if (Target != null)
        //{
        //    int targetID = Target.GetComponent<IDHolder>().UniqueID;
        //    Debug.Log("Target ID: " + targetID);
        //    if (targetID == GlobalSettings.Instance.LowPlayer.PlayerID || targetID == GlobalSettings.Instance.TopPlayer.PlayerID)
        //    {
        //        // attack character
        //        Debug.Log("Attacking " + Target);
        //        Debug.Log("TargetID: " + targetID);
        //        CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID].GoFace();
        //        targetValid = true;
        //    }
        //    else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null)
        //    {
        //        // if targeted creature is still alive, attack creature
        //        targetValid = true;
        //        CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID].AttackCreatureWithID(targetID);
        //        Debug.Log("Attacking " + Target);
        //    }

        //}

        if (!targetValid)
        {
            // not a valid target, return
            //    if (tag.Contains("Low"))
            //        whereIsThisCreature.VisualState = VisualStates.LowTable;
            //    else
            //        whereIsThisCreature.VisualState = VisualStates.TopTable;
            //    whereIsThisCreature.SetTableSortingOrder();
            //}

            // return target and arrow to original position
            transform.localPosition = Vector3.zero;
            sr.enabled = false;
            lr.enabled = false;
            triangleSR.enabled = false;
        }
    }

    // NOT USED IN THIS SCRIPT
    protected override bool DragSuccessful()
    {
        return true;
    }

    internal void ShowAttackInfo(CardDataController attacker)
    {
        string atkInfo = "-" + attacker.Data.Attack;
        info.SetActive(true);
        StartCoroutine(info.GetComponent<InfoTextController>().ShowInfoText(atkInfo, 1));
    }
}
