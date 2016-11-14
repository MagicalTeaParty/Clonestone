using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TestDragDrop : MonoBehaviour
{
    //Hier das DropSpot-Prefab zuweisen
    public GameObject DropSpot;

    //Hier das PlayerBoard-Child des Player-Prefabs zuweisen
    public playerBoardScript PlayerBoard;

    //Arrays der DropSpots
    private GameObject[] dropSpotsOdd = new GameObject[7];
    private GameObject[] dropSpotsEven = new GameObject[6];

    //Hilfsvariable, um die Karten auf dem Board zwischenzuspeichern
    private List<GameObject> droppedCards;

    //Liste der DropSpots
    private List<GameObject> dropSpots;

    void Start()
    {
        for (int i = 0; i < dropSpotsOdd.Length; i++)
        {
            if (i < 6)
            {
                dropSpotsEven[i] = (GameObject)Instantiate(DropSpot);
                dropSpotsEven[i].SetActive(false);
            }
            dropSpotsOdd[i] = (GameObject)Instantiate(DropSpot);
            dropSpotsOdd[i].SetActive(false);
        }

        //Die Positionen der ungeraden DropSpots zuweisen
        dropSpotsOdd[0].transform.position = new Vector3(0f, 0.1f, -0.5f);
        dropSpotsOdd[1].transform.position = new Vector3(-1f, 0.1f, -0.5f);
        dropSpotsOdd[2].transform.position = new Vector3(1f, 0.1f, -0.5f);
        dropSpotsOdd[3].transform.position = new Vector3(-2f, 0.1f, -0.5f);
        dropSpotsOdd[4].transform.position = new Vector3(2f, 0.1f, -0.5f);
        dropSpotsOdd[5].transform.position = new Vector3(-3f, 0.1f, -0.5f);
        dropSpotsOdd[6].transform.position = new Vector3(3f, 0.1f, -0.5f);

        //Die Positionen der geraden DropSpots zuweisen
        dropSpotsEven[0].transform.position = new Vector3(-0.5f, 0.1f, -0.5f);
        dropSpotsEven[1].transform.position = new Vector3(0.5f, 0.1f, -0.5f);
        dropSpotsEven[2].transform.position = new Vector3(-1.5f, 0.1f, -0.5f);
        dropSpotsEven[3].transform.position = new Vector3(-1.5f, 0.1f, -0.5f);
        dropSpotsEven[4].transform.position = new Vector3(-2.5f, 0.1f, -0.5f);
        dropSpotsEven[5].transform.position = new Vector3(-2.5f, 0.1f, -0.5f);
    }

    void Update()
    {
        //Alle DropSpots zur Liste hinzufügen
        dropSpots = GameObject.FindGameObjectsWithTag("DropSpot").ToList();

        droppedCards = GameObject.FindGameObjectsWithTag("Card").ToList();

        //Wenn CardsOnBoard ungerade ist, und die Anzahl der DropSpots gerade, dann
        if (PlayerBoard.CardsOnBoard.Count != 0 && PlayerBoard.CardsOnBoard.Count % 2 != 0 && dropSpots.Count % 2 == 0)
        {
            for (int i = 0; i < dropSpotsEven.Length; i++)
            {
                //Weise der Hilfsvariable droppedCards die Position der ungeraden DropSpots zu
                droppedCards[i].transform.position = dropSpotsOdd[i].transform.position;
                //Zerstöre alle geraden dropSpots
                dropSpotsEven[i].SetActive(false);
            }
            for (int i = 0; i < PlayerBoard.CardsOnBoard.Count; i++)
            {
                //Instanziere alle ungeraden DropSpots
                dropSpotsOdd[i].SetActive(true);
                //Weise den Karten auf dem Board die Position aus der Hilfsvariable zu
                PlayerBoard.CardsOnBoard[i].transform.position = droppedCards[i].transform.position;
            }
        }
        //das genaue Gegenteil
        else if (PlayerBoard.CardsOnBoard.Count != 0 && PlayerBoard.CardsOnBoard.Count % 2 == 0 && dropSpots.Count % 2 != 0)
        {
            for (int i = 0; i < dropSpotsOdd.Length; i++)
            {
                droppedCards[i].transform.position = dropSpotsEven[i].transform.position;
                dropSpotsOdd[i].SetActive(false);
            }
            for (int i = 0; i < PlayerBoard.CardsOnBoard.Count; i++)
            {
                dropSpotsEven[i].SetActive(true);
                PlayerBoard.CardsOnBoard[i].transform.position = droppedCards[i].transform.position;
            }
        }
    }
}