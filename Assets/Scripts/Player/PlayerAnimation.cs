using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private CapsuleCollider2D collider2D;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(rigidBody.velocity.x < -0.2)
        {
            if(spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }
        if(rigidBody.velocity.x > 0.2)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        anim.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        anim.SetFloat("yVelocity", rigidBody.velocity.y);
        anim.SetBool("isGround", Player.instance.getIsGrounded());

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && Player.instance.getIsGrounded())){
            anim.SetTrigger("crouch");
            collider2D.size *= 0.5f;
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            anim.SetTrigger("uncrouch");
            collider2D.size *= 2f;
        }
        
    }
}
