using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class CharacterAnimationController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchSpeed = 2f;

    [Header("Jump")]
    public float jumpForce = 15f;
    public float coyoteTime = 0.08f;
    public float fallMultiplier = 2.2f;
    public float lowJumpMultiplier = 1.5f;

    [Header("Launch Boost")]
    public float launchDuration = 0.5f;
    public float launchGravityFactor = 0.2f;

    [Header("Crouch Collider")]
    public float crouchHeight = 1.0f;
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    // ✅ SAME SOUND FOR ALL ACTIONS
    [Header("Robot Movement Sound")]
    public AudioClip movementSound;

    public float stepIntervalWalk = 0.5f;
    public float stepIntervalRun = 0.3f;
    public float stepIntervalCrouch = 0.7f;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col;
    Keyboard kb;

    AudioSource audioSource;
    float stepTimer;

    float defaultHeight;
    Vector3 defaultCenter;
    float coyoteCounter;

    bool grounded;
    bool crouching;
    bool launched;
    bool running;
    float launchTime;

    int lastDir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        kb = Keyboard.current;

        rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, -20f, 0);

        defaultHeight = col.height;
        defaultCenter = col.center;

        // ✅ Setup Audio
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        // === INPUT ===
        float move = 0;
        if (kb.aKey.isPressed) move = -1;
        if (kb.dKey.isPressed) move = 1;

        crouching = kb.leftCtrlKey.isPressed;

        // === GROUND CHECK ===
        grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f,
                                   Vector3.down, 0.35f);

        running = kb.leftShiftKey.isPressed && grounded;

        // === COYOTE TIME ===
        if (grounded) coyoteCounter = coyoteTime;
        else coyoteCounter -= Time.deltaTime;

        // === FACING ===
        if (move < 0) lastDir = -1;
        if (move > 0) lastDir = 1;

        transform.rotation = (lastDir == 1)
            ? Quaternion.Euler(0, 90, 0)
            : Quaternion.Euler(0, 270, 0);

        // === JUMP SOUND + JUMP ===
        if (kb.spaceKey.wasPressedThisFrame && coyoteCounter > 0 && !launched)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);

            // ✅ Play same sound on jump
            audioSource.PlayOneShot(movementSound);

            anim.SetBool("IsJumping", true);
        }

        // === SPEED ===
        float speed = walkSpeed;
        if (crouching) speed = crouchSpeed;
        else if (running) speed = runSpeed;

        rb.linearVelocity = new Vector3(move * speed, rb.linearVelocity.y, 0);

        // === CROUCH COLLIDER ===
        if (crouching && grounded)
        {
            col.height = crouchHeight;
            col.center = crouchCenter;
        }
        else
        {
            col.height = defaultHeight;
            col.center = defaultCenter;
        }

        // === ANIMATION ===
        anim.SetFloat("MoveSpeed", Mathf.Abs(move));
        anim.SetBool("IsRunning", running);
        anim.SetBool("IsCrouching", crouching);
        anim.SetBool("IsJumping", !grounded);

        // ✅ FOOTSTEP SOUND FOR WALK/RUN/CROUCH
        HandleMovementSound(move);

        // === PLANE LOCK ===
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    // ✅ SAME SOUND FOR WALK + RUN + CROUCH
    void HandleMovementSound(float move)
    {
        bool isMoving = Mathf.Abs(move) > 0.1f;

        if (grounded && isMoving)
        {
            stepTimer -= Time.deltaTime;

            // Choose interval depending on state
            float interval = stepIntervalWalk;

            if (running)
                interval = stepIntervalRun;
            else if (crouching)
                interval = stepIntervalCrouch;

            if (stepTimer <= 0f)
            {
                audioSource.PlayOneShot(movementSound);
                stepTimer = interval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    // BOOST
    public void Launch(float force)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, force, 0);
        launched = true;
        launchTime = launchDuration;

        // ✅ Play same sound on launch
        audioSource.PlayOneShot(movementSound);

        anim.SetBool("IsJumping", true);
    }
}
