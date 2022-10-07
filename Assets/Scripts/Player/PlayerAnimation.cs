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
    private Camera theCam;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = Player.instance.GetComponent<Transform>();
        theCam = Camera.main;
        initialBoxSize = collider2D.size;
        initialBoxOffSet = collider2D.offset;
    }

    Vector3 mousePosition;
    Vector3 playerPosition;
    Transform playerTransform;
    Vector2 initialBoxSize;
    Vector2 initialBoxOffSet;
    void Update()
    {
        mousePosition = Input.mousePosition;
        playerTransform = Player.instance.GetComponent<Transform>();
        playerPosition = theCam.WorldToScreenPoint(playerTransform.localPosition);

        if (mousePosition.x < playerPosition.x)
        {
            // flip sprite if mouse behind
            playerTransform.localScale = new Vector3(-1f, playerTransform.localScale.y, playerTransform.localScale.z);
            
        }
        else
        {
            playerTransform.localScale = new Vector3(1f, playerTransform.localScale.y, playerTransform.localScale.z);
            
        }



        /*if(rigidBody.velocity.x < -0.2)
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
        }*/
        anim.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        anim.SetFloat("yVelocity", rigidBody.velocity.y);
        anim.SetBool("isGround", Player.instance.getIsGrounded());

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && Player.instance.getIsGrounded())){
            anim.SetTrigger("crouch");
            Vector2 crouchBox = new Vector2(initialBoxSize.x, initialBoxSize.y / 2);
            collider2D.size = crouchBox;
            Vector2 crouchOffSet = new Vector2(initialBoxOffSet.x, -0.26f);
            collider2D.offset = crouchOffSet;
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            anim.SetTrigger("uncrouch");
            // -0.26
            collider2D.size = initialBoxSize;
            collider2D.offset = initialBoxOffSet;
        }
        
    }
}
