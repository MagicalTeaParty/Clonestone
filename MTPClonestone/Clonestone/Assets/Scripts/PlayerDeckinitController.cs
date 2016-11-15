using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

   
    private class Wrapper<T>
    {
        public T[] array;
    }
}


public class PlayerDeckinitController : MonoBehaviour {

    
    public void onClick()
    {
        StartCoroutine("getDeck");
    }


    public IEnumerator getDeck()
    {
        CardDataController.CardData[] data;

        string jsonstring = "[{\"IdDeck\":1,\"DeckName\":\"default 1\",	\"IdClass\":7,	\"Class\":\"Paladin\",	\"IdType\":2,	\"TypeName\":\"Hero\",	\"IdCard\":705,	\"CardName\":\"Paladin\",	\"Mana\":0, \"Attack\":0,	\"Health\":30,		\"Flavour\":null, \"IdDeckCard\":31,	\"FileName\":\"705.png\",	\"zahl\":\"00000000-0000-0000-0000-000000000000\" }, {\"IdDeck\":1,\"DeckName\":\"default 1\",	\"IdClass\":7,	\"Class\":\"Paladin\",	\"IdType\":2,	\"TypeName\":\"Hero\",	\"IdCard\":705,	\"CardName\":\"Paladin\",	\"Mana\":0, \"Attack\":0,	\"Health\":30,		\"Flavour\":null, \"IdDeckCard\":31,	\"FileName\":\"705.png\",	\"zahl\":\"00000000-0000-0000-0000-000000000000\" }]";

        //data = JsonUtility.FromJson<List<CardDataController.CardData>>(jsonstring);

        data = JsonHelper.getJsonArray<CardDataController.CardData>(jsonstring);


        //data = JsonUtility.FromJson<CardDataController.CardData>("{\"IdDeck\":1,\"DeckName\":\"default 1\",	\"IdClass\":7,	\"Class\":\"Paladin\",	\"IdType\":2,	\"TypeName\":\"Hero\",	\"IdCard\":705,	\"CardName\":\"Paladin\",	\"Mana\":0, \"Attack\":0,	\"Health\":30,		\"Flavour\":null, \"IdDeckCard\":31,	\"FileName\":\"705.png\",	\"zahl\":\"00000000-0000-0000-0000-000000000000\" }");

        string url = "http://localhost:53861/Deck/GetDeck";
        WWWForm form = new WWWForm();
        form.AddField("idDeck", 1);

        WWW www = new WWW(url, form);
        Debug.Log("hallo");

        yield return www;


        //List<CardDataController.CardData> data = JsonUtility.FromJson<List<CardDataController.CardData>>(www.text);


          
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
