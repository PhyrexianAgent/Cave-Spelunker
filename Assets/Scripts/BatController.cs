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

    public void HeardquietSound()
    {
        if (currentState == BatStates.Wakening)
        {
            SetState(BatStates.FlyStart);
        }
        else
        {
            SetState(BatStates.Wakening);
            Invoke("ReturnToSleep", Random.Range(3, 6));
        }
    }

    private void ReturnToSleep()
    {
        if (currentState == BatStates.Wakening)
        {
            SetState(BatStates.Sleep);
        }
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
                if (!groupController.IsAwakening())
                    groupController.AwakenOthers();
                break;
            case BatStates.Wakening:
                anim.SetBool("Wakening", true);
                break;
            case BatStates.Sleep:
                anim.SetBool("Wakening", false);
                break;
            case BatStates.Chase:
                attackArea.AbleToAttack();
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

