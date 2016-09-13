using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

class cardDrag : NetworkBehaviour
{
    //private Color mouseOverColor = Color.blue;
    //private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;


    //void OnMouseEnter()
    //{
    //    GetComponent<Renderer>().material.color = mouseOverColor;
    //}

    //void OnMouseExit()
    //{
    //    GetComponent<Renderer>().material.color = originalColor;
    //}

    void OnMouseDown()
    {
        // Vector2 und nicht Vector3, um nur die x- und y-Koordinaten der Karte zu verändern und nicht auch die z-Koordinate. Die Karte wird sonst bei jedem Klick weiter in die Ferne gerückt.
        distance = Vector2.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // s. OnMouseDown()
            Vector2 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}