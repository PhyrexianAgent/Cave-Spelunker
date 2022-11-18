using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatListenerController : MonoBehaviour
{
    public BatController owner;

    

    private LayerMask detectLayers;

    void Start()
    {
        detectLayers = LayerMask.GetMask("Player", "Ground", "Walls");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Awakenable()
    {
        return owner.CompareState(BatStates.Sleep) || owner.CompareState(BatStates.Wakening);
    }


    public void AwakenBat()
    {
        if (PlayerSeeable())
        {
            owner.SetState(BatStates.FlyStart);
        }
        else
        {
            Debug.Log("player not visible");
        }
    }

    private bool PlayerSeeable()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, Player.instance.transform.position, detectLayers);
        Debug.Log(hit.collider.gameObject.tag);
        return hit.collider.gameObject.tag == "Player" ;
    }
}
