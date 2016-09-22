using UnityEngine;
using UnityEngine.Networking;

class cardDrag : NetworkBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalScale;

    //Scale the card up when the mouse hovers over it
    void OnMouseEnter()
    {
        if (isLocalPlayer)
        {
            //Save object's current scale
            originalScale = transform.localScale;

            //Scale up until vector3.x reaches 0.8
            while (transform.localScale.x < 0.8)
            {
                transform.localScale *= 1.01f;
            } 
        }
    }

    void OnMouseExit()
    {
        if (isLocalPlayer)
        {
            //Restore object's original scale
            gameObject.transform.localScale = originalScale;
        }
    }

    //Make the card draggable with the mouse
    void OnMouseDown()
    {
        if (isLocalPlayer)
        {
            //Save object's position
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            //Save difference between the mouse' and the object's positions
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (isLocalPlayer)
        {
            //Save the current mouse position
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //Convert the screen point to world point plus the difference between mouse and object
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            //Set the object's position to the new position
            transform.position = curPosition;
        }
    }
}