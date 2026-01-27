using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterAnimationController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;

    Rigidbody rb;
    Animator anim;
    Keyboard kb;
    CapsuleCollider col;

    bool grounded;
    int lastDir = 1;   // 1 = right, -1 = left

    float defaultHeight;
    Vector3 defaultCenter;
    float crouchHeight = 1.0f;
    Vector3 crouchCenter = new Vector3(0, 0.5f, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        kb = Keyboard.current;
        col = GetComponent<CapsuleCollider>();

        defaultHeight = col.height;
        defaultCenter = col.center;

        rb.freezeRotation = true;
    }

    void Update()
    {
        float move = 0;
        if (kb.aKey.isPressed) move = -1;
        if (kb.dKey.isPressed) move = 1;

        // Ground check
        grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);

        bool crouching = kb.leftCtrlKey.isPressed;
        bool running = kb.leftShiftKey.isPressed && grounded && !crouching;

        // Facing
        if (move < 0) lastDir = -1;
        if (move > 0) lastDir = 1;

        if (lastDir == 1)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else
            transform.rotation = Quaternion.Euler(0, 270, 0);

        // Jump
        if (kb.spaceKey.wasPressedThisFrame && grounded && !crouching)
        {
            anim.SetTrigger("Jump");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
        }

        // Speed selection priority
        float speed = walkSpeed;
        if (crouching) speed = crouchSpeed;
        else if (running) speed = runSpeed;

        // Movement
        rb.linearVelocity = new Vector3(move * speed, rb.linearVelocity.y, 0);

        // Collider crouch sizing
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

        // Animator parameters
        anim.SetFloat("MoveSpeed", Mathf.Abs(move));
        anim.SetBool("IsRunning", running);
        anim.SetBool("IsCrouching", crouching);
        anim.SetBool("IsJumping", !grounded);
        anim.SetFloat("VerticalSpeed", rb.linearVelocity.y);

        // Lock Z plane
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
