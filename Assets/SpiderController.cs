using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    [Header("Component References")]
    public SpiderAttackArea attackArea;
    public SpiderSoundDetect soundArea;
    public LineRenderer lineRender;

    [Header("Line Info")]
    public float roofYPos;
    public Vector2 localEndPos;

    

    private SpiderState currentState = SpiderState.Idle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRender.SetPosition(0, new Vector3(localEndPos.x + transform.position.x, roofYPos));
        lineRender.SetPosition(1, (Vector3)localEndPos + transform.position);
    }

    public enum SpiderState 
    {
        Idle,
        Dropping,
        Returning
    }

}
