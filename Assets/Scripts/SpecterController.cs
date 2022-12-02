using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterController : MonoBehaviour
{
    [Header("Kill Player Vars")]
    public bool canKillPlayer = false;
    public float stopRange = 10;
    public Vector2 killOffset = new Vector2(0, 1);

    [Header("FollowingVars")]
    [SerializeField] private float followSpeed = 4;

    [Header("Component Refs")]
    public Rigidbody2D rigid;
    public CapsuleCollider2D capsuleColl;
    public BoxCollider2D boxColl;
    public SpriteRenderer render;
    public Animator anim;
    public DialogueText dialog;

    private Color invisColor;

    private SpecterState currentState = SpecterState.Hiding;
    void Start()
    {
        invisColor = new Color(render.color.r, render.color.g, render.color.b, 0);
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
                if (!Player.instance.encounteredSpecter)
                    StartCoroutine(PlayerDialogueDetector.instance.TriggerDialog(dialog));
                Player.instance.encounteredSpecter = true;
                break;
            case SpecterState.Killing:
                Player.instance.ChangePlayerDialogLock(true);
                rigid.velocity = Vector2.zero;
                render.enabled = false;
                transform.position = Player.instance.transform.position + (Vector3)killOffset;
                Invoke("StartKill", 0.8f);
                break;
        }
        currentState = state;
    }

    private void StartKill()
    {
        render.enabled = true;
        anim.SetTrigger("Killing");
    }

    public void KillingDone()
    {
        Player.instance.Die();
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = Player.instance.transform.position;
        Vector3 moveDir = (playerPos - transform.position).normalized;
        rigid.velocity = moveDir * followSpeed;
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 4);
        render.flipX = rigid.velocity.x < 0;
        if (!canKillPlayer && Vector3.Distance(playerPos, transform.position) <= stopRange)
        {
            rigid.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Player":
                if (currentState != SpecterState.Hiding && canKillPlayer)
                    SetState(SpecterState.Killing);
                break;
        }
    }

    public void LightHitSpecter()
    {
        if (currentState == SpecterState.Following)
            SetState(SpecterState.Hiding);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentState == SpecterState.Hiding && collision.tag != "Light" && collision.tag == "Player")
        {
            SetState(SpecterState.Following);
        }
    }
}
public enum SpecterState
{
    Following,
    Hiding,
    Killing
}
