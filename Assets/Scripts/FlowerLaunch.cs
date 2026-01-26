using UnityEngine;

public class FlowerLaunch : MonoBehaviour
{
    public float launchForce = 22f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPlayer"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
            }
        }
    }
}
