using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    private const float MAX_GROUND_TEST_DIST = 1.68f;
    private const float FALL_MULTIPLIER = 2.5f;
    private const float LOW_JUMP_MULTIPLIER = 2f;
    private const float JUMP_SIZE_MULT = 0.785f;

    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float walkSoundSize = 3;
    [SerializeField] private float walkSoundDamage = 10;
    [SerializeField] private float jumpSoundDamage = 10;
    private bool isGrounded = true;

    [SerializeField] private float jumpSpeed;
    public Rigidbody2D rb;
    public GameObject soundPrefab;
    public GameObject flashlight;

    public bool positionLocked;
    //public AudioSource playerSounds;
    //public AudioClip jump;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
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
        Jump();
        BetterJump();
        TestForGrounded();

        RotateFlashlight();
        if (Input.GetKeyDown(KeyCode.J))
        {
            GenerateSound(walkSoundDamage, walkSoundSize);
        }
    }

    private void TestForGrounded()
    {
        bool oldGrounded = isGrounded;
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, MAX_GROUND_TEST_DIST, groundLayerMask);
        if (!oldGrounded && isGrounded)
        {
            Debug.Log(GetComponent<Rigidbody2D>().velocity.y * JUMP_SIZE_MULT);
            GenerateSound(jumpSoundDamage, Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y * JUMP_SIZE_MULT)); // done this way to make sure jumping up to ledges makes a smaller sound then landing from height
        }
    }

    private void RotateFlashlight()
    {
        Debug.Log(Camera.main == null);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleBetweenPoints = GetAngleBetweenPoints(mousePos, transform.position);
        flashlight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBetweenPoints - 90));
    }

    private float GetAngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
    void Jump()
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w")) && isGrounded)
        {
            //playerSounds.PlayOneShot(jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }
    void BetterJump()
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

    private void GenerateSound(float damage, float size)
    {
        GameObject soundMade = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        soundMade.GetComponent<Sound>().GenerateSound(damage, size);
    }

    public bool getIsGrounded()
    { // Used for animation purposes
        return isGrounded;
    }
}
