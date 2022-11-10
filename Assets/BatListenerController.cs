using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatListenerController : MonoBehaviour
{
    public BatController owner;
    void Start()
    {
        
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
        owner.SetState(BatStates.FlyStart);
    }
}
