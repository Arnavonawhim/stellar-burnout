using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 25f; // Increase for VERY HIGH jump

    private void OnTriggerEnter(Collider other)
    {
        // Check if player touches pad
        if (other.CompareTag("DarkPlayer"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Reset current Y velocity for clean bounce
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

                // Launch player upward
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
