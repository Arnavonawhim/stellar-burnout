using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float crouchSpeed = 3f;
    public float jumpForce = 12f;
    public int maxJumps = 2;

    Rigidbody rb;
    Vector3 planeZ;
    int jumpsRemaining;
    bool crouching;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planeZ = new Vector3(0, 0, transform.position.z);
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, planeZ.z);

        float x = Input.GetAxisRaw("Horizontal");
        float s = crouching ? crouchSpeed : speed;

        rb.linearVelocity = new Vector3(x * s, rb.linearVelocity.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftControl))
            crouching = true;

        if (Input.GetKeyUp(KeyCode.LeftControl))
            crouching = false;
    }

    void Jump()
    {
        if (jumpsRemaining <= 0) return;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, 0);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpsRemaining--;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.contacts[0].normal.y > 0.5f)
            jumpsRemaining = maxJumps;
    }
}
