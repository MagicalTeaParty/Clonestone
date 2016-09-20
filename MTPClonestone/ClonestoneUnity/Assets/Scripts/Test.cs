using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject text = GameObject.Find("Text");

        text.GetComponent<Text>().text = "Sourcecode";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
