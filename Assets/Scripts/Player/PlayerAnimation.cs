using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const float MAX_WALK_TIME = 0.5f;

    [SerializeField]
    private Rigidbody2D rigidBody;
    private Animator anim;
    [SerializeField] private float walkSoundSize = 8;
    [SerializeField] private float walkSoundDamage = 10;
    //private SpriteRenderer spriteRenderer;
    [SerializeField]
    public CapsuleCollider2D collider2D;
    private Camera theCam;
    private Transform playerTransform;
    public GameObject soundPrefab;

    private float walkTime = MAX_WALK_TIME;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = Player.instance.GetComponent<Transform>();
        theCam = Camera.main;
        initialBoxSize = collider2D.size;
        initialBoxOffSet = collider2D.offset;
    }

    Vector2 initialBoxSize;
    Vector2 initialBoxOffSet;
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        playerTransform = Player.instance.GetComponent<Transform>();
        Vector2 playerPosition = theCam.WorldToScreenPoint(playerTransform.localPosition);
        playerTransform.localScale = new Vector3(mousePosition.x < playerPosition.x  ? - 1f : 1f, playerTransform.localScale.y, playerTransform.localScale.z);

        anim.SetFloat("xVelocity", Mathf.Abs(rigidBody.velocity.x));
        anim.SetFloat("yVelocity", rigidBody.velocity.y);
        anim.SetBool("isGround", Player.instance.getIsGrounded());
        anim.SetFloat("speed", Mathf.Abs(Player.instance.getMoveBy()));
        MovementDetects();
        TestWalk();
    }

    private void TestWalk()
    {
        if (anim.GetFloat("xVelocity") > 0.01f && anim.GetFloat("yVelocity") != 0)
        {
            walkTime -= Time.deltaTime;
            if (walkTime <= 0)
            {
                walkTime = MAX_WALK_TIME;
                CreateSound();
            }
        }
        else if (walkTime != MAX_WALK_TIME)
        {
            walkTime = MAX_WALK_TIME;
        }


    }

    private void MovementDetects()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && Player.instance.getIsGrounded()))
        {
            anim.SetTrigger("crouch");
            Vector2 crouchBox = new Vector2(initialBoxSize.x * 2, initialBoxSize.y / 2);
            collider2D.size = crouchBox;
            Vector2 crouchOffSet = new Vector2(initialBoxOffSet.x, -0.26f);
            collider2D.offset = crouchOffSet;
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetTrigger("uncrouch");
            collider2D.size = initialBoxSize;
            collider2D.offset = initialBoxOffSet;
        }
    }

    private void CreateSound() // will change a bit when running is added to have running make a louder noise
    {
        GameObject newSound = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        newSound.GetComponent<Sound>().GenerateSound(walkSoundDamage, walkSoundSize);
    }
}
