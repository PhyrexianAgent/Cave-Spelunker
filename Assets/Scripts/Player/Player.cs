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
    private const float SPRINT_SPEED_MULT = 2;
    private const float SNEAK_SPEED_MULT = 0.5f;

    [SerializeField] private float speed;
    private float moveBy;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float walkSoundSize = 3;
    [SerializeField] private float walkSoundDamage = 10;
    [SerializeField] private float jumpSoundDamage = 10;
    [SerializeField] private float climbSpeed = 7;
    [SerializeField] private float grappleDescendSpeed = 3;
    [SerializeField] private GrapplingGun grapplingGun;
    private bool isGrounded = true;
    private bool isDead = false;

    private PlayerStates currentState = PlayerStates.Normal;

    [SerializeField] private float jumpSpeed;
    public Rigidbody2D rb;
    public GameObject soundPrefab;
    public GameObject flashlight;
    public GrapplingGun grappleGun;
    
    //public AudioSource playerSounds;
    //public AudioClip jump;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //PlayerDeathMenuController.instance.PlayerDied();
        //playerSounds = this.GetComponent<AudioSource>();
    }

    private void SetCurrentState(PlayerStates state)
    {
        if (currentState == PlayerStates.Grappled && grappleGun.GetIsGrappling() && state != currentState)
        {
            //Debug.Log("disabling grapple");
            grappleGun.DisableGrapple();
        }
        currentState = state;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DoStateActions();
        MoveNormally();
        TestForGrounded();
        RotateFlashlight();
        if (Input.GetKeyDown(KeyCode.J))
        {
            GenerateSound(walkSoundDamage, walkSoundSize);
        }
    }

    private void DoStateActions()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case PlayerStates.Normal:
                Jump();
                BetterJump();
                break;
            case PlayerStates.Grappled:
                MoveInGrapple();
                break;
            case PlayerStates.LockedInSpeaking:
                rb.velocity = Vector2.zero;
                break;
        }
    }

    private void MoveInGrapple()
    {
        grappleGun.GoUp(Input.GetKey(KeyCode.W));
        if (Input.GetKey(KeyCode.S))
            DescendGrapple();
        else
        {
            rb.velocity = Vector2.zero;
            //Debug.Log("not going down");
        }
    }

    private void DescendGrapple()
    {
        rb.velocity = new Vector2(0, -grappleDescendSpeed * Time.deltaTime * 100);
        //Debug.Log(rb.velocity);
    }

    private void MoveNormally()
    {
        Move();
        
    }

    private void TestForGrounded()
    {
        bool oldGrounded = isGrounded;
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, MAX_GROUND_TEST_DIST, groundLayerMask);
        if (!oldGrounded && isGrounded)
        {
            GenerateSound(jumpSoundDamage, Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y * JUMP_SIZE_MULT)); // done this way to make sure jumping up to ledges makes a smaller sound then landing from height
        }
    }

    private void RotateFlashlight()
    {
        //Debug.Log(Camera.main == null);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleBetweenPoints = GetAngleBetweenPoints(mousePos, flashlight.transform.position);
        flashlight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBetweenPoints - 90));
    }

    private float GetAngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float speedMult = GetSpeedMult();
        //Debug.Log(speedMult);
        moveBy = x * speed * speedMult;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    private float GetSpeedMult()
    {
        float speedMult = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speedMult = SPRINT_SPEED_MULT;
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            speedMult = SNEAK_SPEED_MULT;
        }
        return speedMult;
    }
    public bool IsCrouching()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
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

    public float getMoveBy()
    {
        return moveBy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Stalactite":
                if (collision.gameObject.GetComponent<StalactiteBehaviour>().GetIsFalling())
                    Die();
                break;
            case "Spider":
                Die();
                break;
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            CameraFollow.instance.followPlayer = false;
            PlayerDeathMenuController.instance.PlayerDied();
        }
        
    }

    public void ChangePlayerDialogLock(bool isLocked)
    {
        SetCurrentState(isLocked ? PlayerStates.LockedInSpeaking : PlayerStates.Normal);
    }

    public void SetPlayerGrapple(bool isGrappled)
    {
        SetCurrentState(isGrappled ? PlayerStates.Grappled : PlayerStates.Normal);
    }

    private enum PlayerStates 
    {
        Normal,
        Grappled,
        LockedInSpeaking
    }

}
