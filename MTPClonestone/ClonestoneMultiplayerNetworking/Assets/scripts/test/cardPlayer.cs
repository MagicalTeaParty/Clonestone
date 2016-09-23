using UnityEngine.Networking;

class cardPlayer : NetworkBehaviour
{
    //Should only happen for the local player
    public override void OnStartLocalPlayer()
    {
        //Rotate the object up by 180°
        transform.RotateAround(transform.position, transform.up, 180f);
    }
    void Start()
    {
        //The enemy player must not see the player's cards' front
        if (!isLocalPlayer)
        {
            //Set the first child (= plane with active picture) inactive
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}