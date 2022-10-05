using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    private void Awake()
    {
        instance = this;
    }

    private const float FALL_MULTIPLIER = 2.5f;
    private const float LOW_JUMP_MULTIPLIER = 2f;

    [SerializeField] private float speed;
    private bool isGrounded = false;

    public float jumpSpeed;
    public Rigidbody2D rb;

    public bool positionLocked;
    //public Vector2 movement;
    //public GameManager gm;
    //public Transform isGroundedChecker;
    //public float checkGroundRadius;
    //public LayerMask groundLayer;
    //public AudioSource playerSounds;
    //public AudioClip jump;

    // Start is called before the first frame update
    void Start()
    {
        //playerSounds = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!positionLocked)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        //Jump();
        //BetterJump();
        //CheckIfGrounded();
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump() // temporarly disabling for now as we can do jump later
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && isGrounded == true)
        {
            //playerSounds.PlayOneShot(jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    void BetterJump() // temporarly disabling for now as we can do jump later
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (FALL_MULTIPLIER - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (LOW_JUMP_MULTIPLIER - 1) * Time.deltaTime;
        }
    }
/*    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }*/
}
