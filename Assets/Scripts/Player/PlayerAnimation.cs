using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const float MAX_WALK_TIME = 0.6f;
    private const float WALK_START_REMOVE = 0.1f;
    private const float MAX_WALK_RESET_TIME = 1;

    [SerializeField]
    private Rigidbody2D rigidBody;
    public Animator anim;
    [SerializeField] private float walkSoundSize = 8;
    [SerializeField] private float walkSoundDamage = 10;
    [SerializeField] private float sneakSoundDamage = 5;
    [SerializeField] private float runSoundSize = 10;
    //private SpriteRenderer spriteRenderer;
    [SerializeField]
    public CapsuleCollider2D collider2D;
    private Camera theCam;
    private Transform playerTransform;
    public GameObject soundPrefab;

    private float walkTime = MAX_WALK_TIME;
    private float walkResetTime = MAX_WALK_RESET_TIME;
    private bool shimmied = false;

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

        TestForWalkStart();

        //MovementDetects();
        TestWalk();
    }

    private void TestForWalkStart()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !Player.instance.IsInDialogue())
        {
            walkTime -= WALK_START_REMOVE;
            if (Input.GetKey(KeyCode.S))//(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftAlt))
            {
                PlaySneak();
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                PlayRun();
            }
            else
            {
                PlayWalk();
            }
            //PlayWalk();
            //CreateSound(Player.instance.IsSneaking());
        }
    }

    public void PlayWalk()
    {
        if (!Player.instance.IsGrappled())
        {
            CreateSound(false, walkSoundDamage, walkSoundSize);
            AudioManager.instance.Play("Walk");
        }
    }

    public void PlaySneak()
    {
        if (!Player.instance.IsGrappled())
        {
            CreateSound(true, sneakSoundDamage, walkSoundSize);
            AudioManager.instance.Play("Sneak");
        }
    }

    public void PlayRun()
    {
        if (!Player.instance.IsGrappled())
        {
            CreateSound(false, walkSoundDamage, runSoundSize);
            AudioManager.instance.Play("Run");
        }
        
    }

    private void CheckEndWalk()
    {
        if (walkTime <= 0)
        {
            //AudioManager.instance.Play("Walk");
            walkTime = MAX_WALK_TIME;
            //CreateSound(Player.instance.IsSneaking());
        }
    }

    private void TestWalk()
    {
        if (anim.GetFloat("xVelocity") > 0.4f && anim.GetFloat("yVelocity") != 0)
        {
            walkTime -= Time.deltaTime;
            CheckEndWalk();
        }
        else if (walkTime != MAX_WALK_TIME)
        {
            walkResetTime -= Time.deltaTime;
            if (walkResetTime <= 0)
            {
                walkResetTime = MAX_WALK_RESET_TIME;
                walkTime = MAX_WALK_TIME;
            }
        }
        /*else if (walkTime != MAX_WALK_TIME)
        {
            walkTime = MAX_WALK_TIME;
        }*/
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

    private void CreateSound(bool isQuiet, float damage, float size) // will change a bit when running is added to have running make a louder noise
    {
        GameObject newSound = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        newSound.GetComponent<Sound>().GenerateSound(damage, size, isQuiet);
    }
}
