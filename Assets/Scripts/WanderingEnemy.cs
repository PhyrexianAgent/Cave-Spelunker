using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemy : MonoBehaviour
{
    private bool soundHeard;
    public float speed = 2.5f;
    public Vector2 movement;
    public GameManager gm;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        WanderingPhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WanderingPhase()
    {

    }

    void InspectionPhase()
    {

    }
}
