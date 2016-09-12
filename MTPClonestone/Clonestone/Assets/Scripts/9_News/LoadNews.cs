using UnityEngine;
using System.Collections;

public class LoadNews : MonoBehaviour {

    public Vector2 scrollPosition = Vector2.zero;
    public string innerText = "hi";

    void OnGUI()
    {
        //GUI.BeginScrollView(new Rect(0, 0, 200, 200), scrollPosition, new Rect(0, 0, 190, 400));

        //innerText = GUI.TextArea(new Rect(0, 0, 200, 300), innerText);

        //GUI.EndScrollView();
    }

	// Use this for initialization
	void Start () {

       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
