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
    public CapsuleCollider2D capsuleColl;
    public BoxCollider2D boxColl;
    public SpriteRenderer render;
    public Animator anim;

    private SpecterState currentState = SpecterState.Hiding;
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

    public void SetState(SpecterState state)
    {
        switch (state)
        {
            case SpecterState.Following:
                capsuleColl.enabled = true;
                boxColl.enabled = false;
                render.enabled = true;
                break;
            case SpecterState.Hiding:
                capsuleColl.enabled = false;
                boxColl.enabled = true;
                rigid.velocity = Vector2.zero;
                anim.SetTrigger("Death");
                break;
        }
        currentState = state;
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 moveDir = (playerPos - transform.position).normalized;
        rigid.velocity = moveDir * followSpeed;
        render.flipX = rigid.velocity.x < 0;
        if (!canKillPlayer && Vector3.Distance(playerPos, transform.position) <= stopRange)
        {
            rigid.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Light")
        { 
            SetState(SpecterState.Hiding);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentState == SpecterState.Hiding && collision.tag != "Light")
        {
            SetState(SpecterState.Following);
        }
    }
}
public enum SpecterState
{
    Following,
    Hiding
}
