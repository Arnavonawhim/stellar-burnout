using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
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

    [Header("Launch Boost (Flower Boost)")]
    public float launchDuration = 0.5f;       // how long gravity is reduced
    public float launchGravityFactor = 0.2f;  // 20% gravity during boost

    [Header("Crouch Collider")]
    public float crouchHeight = 1.0f;
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col;
    Keyboard kb;

    float defaultHeight;
    Vector3 defaultCenter;
    float coyoteCounter;

    bool grounded;
    bool crouching;
    bool launched;
    bool running;
    float launchTime;

    int lastDir = 1; // 1 = right, -1 = left

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        kb = Keyboard.current;

        defaultHeight = col.height;
        defaultCenter = col.center;

        rb.freezeRotation = true;

        // stronger gravity for realistic platformer
        Physics.gravity = new Vector3(0, -20f, 0);
    }

    void Update()
    {
        // === INPUT ===
        float move = 0;
        if (kb.aKey.isPressed) move = -1;
        if (kb.dKey.isPressed) move = 1;

        crouching = kb.leftCtrlKey.isPressed;
        running = kb.leftShiftKey.isPressed && grounded && !crouching;

        // === GROUND CHECK ===
        grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.35f);

        // === COYOTE TIME ===
        if (grounded) coyoteCounter = coyoteTime;
        else coyoteCounter -= Time.deltaTime;

        // === FACING ===
        if (move < 0) lastDir = -1;
        if (move > 0) lastDir = 1;

        transform.rotation = lastDir == 1 ?
            Quaternion.Euler(0, 90, 0) :
            Quaternion.Euler(0, 270, 0);

        // === NORMAL JUMP ===
        if (kb.spaceKey.wasPressedThisFrame && coyoteCounter > 0 && !crouching && !launched)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
            anim.SetBool("IsJumping", true);
        }

        // === LAUNCH BOOST PHASE ===
        if (launched)
        {
            launchTime -= Time.deltaTime;

            // reduced gravity hang time
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (launchGravityFactor - 1f) * Time.deltaTime;

            if (launchTime <= 0f)
                launched = false;
        }
        else
        {
            // === BETTER FALL ===
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.linearVelocity.y > 0 && !kb.spaceKey.isPressed)
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
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

        // === LAND RESET ===
        if (grounded)
            anim.SetBool("IsJumping", false);

        // === ANIM SYNC ===
        anim.SetFloat("MoveSpeed", Mathf.Abs(move));
        anim.SetBool("IsRunning", running);
        anim.SetBool("IsCrouching", crouching);
        anim.SetBool("IsJumping", !grounded);
        anim.SetFloat("VerticalSpeed", rb.linearVelocity.y);

        // === PLANE LOCK ===
        var pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    // FLOWER LAUNCH FUNCTION
    public void Launch(float force)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, force, 0);
        launched = true;
        launchTime = launchDuration;
        anim.SetBool("IsJumping", true);
    }
}
