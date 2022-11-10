using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [Header("Component References")]
    public SpiderAttackArea attackArea;
    public SpiderSoundDetect soundArea;
    public LineRenderer lineRender;
    public Rigidbody2D rigid;

    [Header("Line Info")]
    public float roofYPos;
    public Vector2 localEndPos;

    [Header("Drop Values")]
    public float fallGravityScale = 4;
    public float returnSpeed = 4;


    private Vector2 startPos;
    private SpiderState currentState = SpiderState.Idle;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lineRender.SetPosition(0, new Vector3(localEndPos.x + transform.position.x, roofYPos));
        lineRender.SetPosition(1, (Vector3)localEndPos + transform.position);
        DoStateActions();
    }

    private void DoStateActions()
    {
        switch (currentState)
        {
            case SpiderState.Returning:
                if (transform.position.y >= startPos.y)
                {
                    currentState = SpiderState.Idle;
                    rigid.velocity = Vector2.zero;
                }
                break;
        }
    }

    public void BeginDescent()
    {
        currentState = SpiderState.Dropping;
        rigid.gravityScale = fallGravityScale;
        rigid.velocity = Vector2.zero;
    }

    public bool CompareState(SpiderState state)
    {
        return state == currentState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Invoke("BeginAscent", 1);
        }
    }

    private void BeginAscent()
    {
        currentState = SpiderState.Returning;
        rigid.gravityScale = 0;
        rigid.velocity = new Vector2(0, returnSpeed);
    }

    public enum SpiderState 
    {
        Idle,
        Dropping,
        Returning
    }

}
