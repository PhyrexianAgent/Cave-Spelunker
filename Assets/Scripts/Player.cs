using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public Vector2 movement;
    public GameManager gm;
    public Rigidbody2D rb;
    public bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public AudioSource playerSounds;
    public AudioClip jump;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0.0f);
        rb = this.GetComponent<Rigidbody2D>();
        playerSounds = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump()
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && isGrounded == true)
        {
            playerSounds.PlayOneShot(jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    void CheckIfGrounded()
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
    }
}
