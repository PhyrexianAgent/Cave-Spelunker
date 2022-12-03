using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    private const float MAX_GROUND_TEST_DIST = 1.68f;
    private const float FALL_MULTIPLIER = 2.5f;
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
    [SerializeField] private float flashlightRayDistance = 10;
    [SerializeField] private float stalactiteDamagedTime = 5;
    [SerializeField] private float stalactiteDamagedSpeedMult = 0.5f;
    private bool isGrounded = true;
    private bool isDead = false;
    public bool encounteredBats = false;
    public bool encounteredSpecter = true;

    private PlayerStates currentState = PlayerStates.Normal;
    private float currentGravScale;

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
        currentGravScale = rb.gravityScale;
    }

    public bool IsGrappled()
    {
        return currentState == PlayerStates.Grappled;
    }

    private void SetCurrentState(PlayerStates state)
    {
        if (currentState == PlayerStates.Grappled && grappleGun.GetIsGrappling() && state != currentState)
        {
            Debug.Log("disabling grapple from set state");
            Debug.Log(state);
            grappleGun.DisableGrapple();
        }
        currentState = state;
    }


    void Update()
    {
        DoStateActions();
        CheckFlashlightRay();
    }

    private void CheckFlashlightRay()
    {
        Vector3 pointDir = flashlight.transform.rotation * Vector3.up;
        RaycastHit2D hit = Physics2D.Raycast(flashlight.transform.position, pointDir, flashlightRayDistance);
        if (hit && hit.collider.gameObject.tag == "Specter")
        {
            hit.collider.GetComponent<SpecterController>().LightHitSpecter();
        }
        Debug.DrawRay(flashlight.transform.position, pointDir * flashlightRayDistance, Color.green);
    }

    private void FixedUpdate()
    {
        
        TestForGrounded();
        if (currentState != PlayerStates.LockedInSpeaking)
            Move();
        RotateFlashlight();

    }

    public bool IsPlayerLocked()
    {
        return currentState == PlayerStates.LockedInSpeaking;
    }

    private void DoStateActions()
    {
        switch (currentState)
        {
            case PlayerStates.Normal:
                Jump();
                break;
            case PlayerStates.Grappled:
                MoveInGrapple();
                break;
            case PlayerStates.LockedInSpeaking:
                Debug.Log("in speaking");
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
    }

    public Vector3 GetCurrentVelocity()
    {
        return rb.velocity;
    }

    private void MoveInGrapple()
    {
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            grappleGun.GoUp(true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            grappleGun.GoUp(false);
        }*/
        grappleGun.GoUp(Input.GetKey(KeyCode.W));
    }

    private void TestForGrounded()
    {
        bool oldGrounded = isGrounded;
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, MAX_GROUND_TEST_DIST, groundLayerMask);
        if (!oldGrounded && isGrounded && GetComponent<Rigidbody2D>().velocity.y < -9.2)
        {
            //Debug.Log(GetComponent<Rigidbody2D>().velocity.y);
            GenerateSound(jumpSoundDamage, Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y * JUMP_SIZE_MULT), false); // done this way to make sure jumping up to ledges makes a smaller sound then landing from height
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
        if (currentState != PlayerStates.Grappled)
        {
            rb.gravityScale = currentGravScale;
        }
        //Debug.Log(rb.gravityScale);
    }

    private float GetSpeedMult()
    {
        float speedMult = 1;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speedMult = SPRINT_SPEED_MULT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            speedMult = SNEAK_SPEED_MULT;
        }
        if (currentState == PlayerStates.DamagedByStalactite)
        {
            speedMult *= stalactiteDamagedSpeedMult;
        }
        return speedMult;
    }
    public bool IsSneaking()
    {
        return false;
    }
    void Jump()
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            AudioManager.instance.Play("Jump");
        }
    }

    private void GenerateSound(float damage, float size, bool isQuiet)
    {
        GameObject soundMade = Instantiate(soundPrefab, transform.position, Quaternion.identity);
        soundMade.GetComponent<Sound>().GenerateSound(damage, size);
    }

    public bool getIsGrounded()
    { // Used for animation purposes
        return isGrounded;
    }

    public bool IsInDialogue()
    {
        return currentState == PlayerStates.LockedInSpeaking;
    }

    public float getMoveBy()
    {
        return IsPlayerLocked() ? 0 : moveBy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Stalactite":
                if (collision.gameObject.GetComponent<StalactiteBehaviour>().GetIsFalling())
                    DamagedbyStalactite();
                break;
            case "Spider":
                Die();
                break;
        }
    }

    private void DamagedbyStalactite()
    {
        if (currentState == PlayerStates.DamagedByStalactite)
        {
            Die();
        }
        else
        {
            SetCurrentState(PlayerStates.DamagedByStalactite);
            PlayerDeathMenuController.instance.anim.SetTrigger("PlayerHurt");
            Invoke("RecoveredFromStalactite", stalactiteDamagedTime);
        }
    }

    private void RecoveredFromStalactite()
    {
        SetCurrentState(PlayerStates.Normal);
    }

    public void Die()
    {
        if (!isDead)
        {
            AudioManager.instance.Play("Dead");
            AudioManager.instance.Stop("Bat");
            isDead = true;
            CameraFollow.instance.followPlayer = false;
            PlayerDeathMenuController.instance.PlayerDied();
            
        }
        
    }

    public void ChangePlayerDialogLock(bool isLocked)
    {
        Debug.Log(isLocked);
        SetCurrentState(isLocked ? PlayerStates.LockedInSpeaking : PlayerStates.Normal);
    }

    public void SetPlayerGrapple(bool isGrappled)
    {
        SetCurrentState(isGrappled ? PlayerStates.Grappled : PlayerStates.Normal);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public bool NotDamagedByStalactite()
    {
        return currentState != PlayerStates.DamagedByStalactite;
    }

    private enum PlayerStates 
    {
        Normal,
        Grappled,
        LockedInSpeaking,
        DamagedByStalactite
    }

}
