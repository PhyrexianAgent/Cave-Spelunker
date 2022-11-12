using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    private const float MAX_SOUND_HEALTH_RESET_TIME = 2;

    [Header("Component References")]
    public Rigidbody2D rigid;
    public BoxCollider2D coll;
    public Animator anim;
    public BatAttackArea attackArea;

    [Header("Chase Vals")]
    [SerializeField] private float followSpeed = 5;

    [Header("Sound Vals")]
    [SerializeField] private float soundHealthStart = 10;

    [Header("Other Vars")]
    public BatGroupController groupController;

    private BatStates currentState = BatStates.Sleep;
    private float soundHealth;

    private float resetSoundHealthTimer = 0;
    void Start()
    {
        soundHealth = soundHealthStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (resetSoundHealthTimer > 0)
        {
            resetSoundHealthTimer -= Time.deltaTime;
            if (resetSoundHealthTimer <= 0)
            {
                soundHealth = soundHealthStart;
            }
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

    public void HeardquietSound(float damage)
    {
        soundHealth -= damage;
        if (soundHealth <= 0)
        {
            QuietSoundHeard();
        }
        else
        {
            resetSoundHealthTimer = MAX_SOUND_HEALTH_RESET_TIME;
        }
    }

    private void QuietSoundHeard()
    {
        if (currentState == BatStates.Wakening)
        {
            SetState(BatStates.FlyStart);
        }
        else
        {
            SetState(BatStates.Wakening);
            Invoke("ReturnToSleep", Random.Range(3, 6));
            Debug.Log("waking");
            soundHealth = soundHealthStart / 2;
        }
    }

    private void ReturnToSleep()
    {
        if (currentState == BatStates.Wakening)
        {
            SetState(BatStates.Sleep);
            soundHealth = soundHealthStart;
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

