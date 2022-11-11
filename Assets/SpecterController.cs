using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterController : MonoBehaviour
{
    [Header("Can Kill Player Vars")]
    public bool canKillPlayer = false;
    public float stopRange = 10;

    [Header("FollowingVars")]
    [SerializeField] private float followSpeed = 4;

    [Header("Component Refs")]
    public Rigidbody2D rigid;

    private SpecterState currentState = SpecterState.Following;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (currentState == SpecterState.Following)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 moveDir = (playerPos - transform.position).normalized;
        rigid.velocity = moveDir * followSpeed;
    }
}
public enum SpecterState
{
    Following,
    Hiding
}
