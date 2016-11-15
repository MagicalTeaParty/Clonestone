using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class dropHandler : NetworkBehaviour
{
    //Dieser Variable muss ich in Unity das Prefab zuordnen (reinziehen); muss deshalb public sein!
    public GameObject DropSpot;
    private List<GameObject> dropSpots;

    private List<GameObject> cards;
    private GameObject card;

    private int maxCardsOnBoard = 7;

    public playerBoardScript PlayerBoard;

    //Wird für Erkennung von Objekten benötigt
    private Ray ray;
    private RaycastHit hit;

    private Vector3 shiftDropSpot = new Vector3(-1f, 0f, 0f);

    void Start()
    {
        if (!isLocalPlayer)
            return;
        //Hier wird der erste DropSpot instanziert
        //Instantiate(DropSpot);
        DropSpot.transform.position = new Vector3(0f,0.1f,-0.5f);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        //Suche alle DropSpots und füge sie zur Liste dropSpots hinzu
        dropSpots = GameObject.FindGameObjectsWithTag("DropSpot").ToList();

        #region Assign Card to DropSpot

        //Ändere den Strahl anhand der aktuellen Mausposition
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Jeden Frame wird der Strahl "ray" 10 Längeneinheiten weit geschossen
        if (Physics.Raycast(ray, out hit, 50))
        {
            //Trifft der Strahl auf den Collider eines GameObjects, so wird dieses der GameObject Variable "card" zugewiesen
            card = hit.collider.gameObject;

            //Hier werden alle GameObjects mit dem Tag "Card" einem Array zugewiesen, in eine Liste umgewandelt und auf die Listenvariable "cards" gespeichert
            cards = GameObject.FindGameObjectsWithTag("Card").ToList();

            //Wenn "card" in "cards" vorkommt
            if (cards.Contains(card))
            {
                //durchlaufe die Liste dropSpots
                for (int i = 0; i < dropSpots.Count; i++)
                {
                    //Wenn die Distanz zwischen der Karte und dem dropSpots[i] kleiner 1 ist
                    if (Vector3.Distance(card.transform.position, dropSpots[i].transform.position) < 1.0f)
                    {
                        //Weise der Karte die Position des dropSpots[i] zu
                        card.transform.position = dropSpots[i].transform.position;
                    }
                }
            }
        }

        if (PlayerBoard.CardsOnBoard.Count == 0)
        {
            Destroy(dropSpots.FirstOrDefault());
        }

        #endregion

        #region Create DropSpots

        if (PlayerBoard.CardsOnBoard.Count > 0)
        {
            for (int i = 0; i < maxCardsOnBoard; i++)
            {
                if (PlayerBoard.CardsOnBoard.Count == i+1 && dropSpots.Count == i)
                {
                    Instantiate(DropSpot);
                    DropSpot.transform.position = new Vector3(i, 0.1f, -0.5f);
                    foreach (GameObject dropSpot in dropSpots)
                    {
                        dropSpot.transform.position += shiftDropSpot;
                    }
                }
                if (PlayerBoard.CardsOnBoard.Count == 1)
                {
                    DropSpot.transform.position = new Vector3(0f, 0.1f, -0.5f);
                }
            }
        }

        #endregion

        if (PlayerBoard.CardsOnBoard.Count + 1 <= dropSpots.Count)
        {
            Destroy(dropSpots.First());
        }
    }
}