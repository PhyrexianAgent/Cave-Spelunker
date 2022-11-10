using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [Header("Component References")]
    public Rigidbody2D rigid;
    public BoxCollider2D coll;
    public Animator anim;
    public BatAttackArea attackArea;

    [Header("Chase Vals")]
    [SerializeField] private float followSpeed = 5;

    [Header("Other Vars")]
    public BatGroupController groupController;

    private BatStates currentState = BatStates.Sleep;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == BatStates.Wakening)
        {

        }
    }

    private void FixedUpdate()
    {
        if (currentState == BatStates.Chase)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 diffNorm = (playerPos - transform.position).normalized;
        rigid.velocity = diffNorm * followSpeed;
    }

    public bool CompareState(BatStates state)
    {
        return currentState == state;
    }

    public void SetState(BatStates state)
    {
        switch (state)
        {
            case BatStates.FlyStart:
                anim.SetBool("Awake", true);
                attackArea.AbleToAttack();
                //coll.enabled = true;
                if (!groupController.IsAwakening())
                    groupController.AwakenOthers();
                break;
        }
        currentState = state;
    }
}

public enum BatStates 
{
    Sleep,
    Wakening,
    FlyStart,
    Chase
}

