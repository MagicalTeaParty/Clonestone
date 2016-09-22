using UnityEngine;
using UnityEngine.Networking;

class cardDrag : NetworkBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPosition;

    //Change z-coordinate when the mouse hovers over it
    void OnMouseEnter()
    {
        if (isLocalPlayer)
        {
            originalPosition = transform.position;
            transform.position = originalPosition + new Vector3(0f, 0f, -0.1f);
        }
    }

    void OnMouseExit()
    {
        if (isLocalPlayer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);
        }
    }

    //Make the card draggable with the mouse
    void OnMouseDown()
    {
        if (isLocalPlayer)
        {
            //Save object's position
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            //Save difference between the mouse' and the object's positions
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
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
            if (curPosition.x < 0)
            {
                transform.position = curPosition;
            }
            else transform.position = originalPosition;
        }
    }
}