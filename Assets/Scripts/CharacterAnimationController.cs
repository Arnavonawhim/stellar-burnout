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
    int lastDir = -1; // 1 = right, -1 = left    

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

        bool running = kb.leftShiftKey.isPressed;
        bool crouching = kb.leftCtrlKey.isPressed;

        // Facing logic
        if (move < 0) lastDir = 1;
        if (move > 0) lastDir = -1;

        if (lastDir == 1)
            transform.rotation = Quaternion.Euler(0, 270, 0);
        else
            transform.rotation = Quaternion.Euler(0, 90, 0);

        // Ground check
        grounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);

        // Jump
        if (kb.spaceKey.wasPressedThisFrame && grounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
        }

        if (grounded)
            anim.SetBool("IsJumping", false);

        // Movement speed
        float speed = walkSpeed;
        if (running && !crouching) speed = runSpeed;
        if (crouching) speed = crouchSpeed;

        rb.linearVelocity = new Vector3(move * speed, rb.linearVelocity.y, 0);

        // Collider crouch change
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

        // Animator params
        anim.SetFloat("MoveSpeed", Mathf.Abs(move));
        anim.SetBool("IsRunning", running);
        anim.SetBool("IsCrouching", crouching);
        anim.SetBool("IsJumping", !grounded);

        // Lock Z plane
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
