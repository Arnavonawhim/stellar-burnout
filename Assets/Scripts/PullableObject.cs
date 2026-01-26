using UnityEngine;

public class PullableObject : MonoBehaviour
{
    public float pullSpeed = 5f;
    Rigidbody rb;
    Transform pullTarget;
    bool isBeingPulled;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isBeingPulled || pullTarget == null) return;

        rb.useGravity = false;

        Vector3 direction = (pullTarget.position - transform.position).normalized;
        rb.linearVelocity = direction * pullSpeed;

        transform.position = new Vector3(transform.position.x, transform.position.y, pullTarget.position.z);
    }

    public void StartPull(Transform target)
    {
        pullTarget = target;
        isBeingPulled = true;
    }

    public void StopPull()
    {
        isBeingPulled = false;
        pullTarget = null;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
    }
}
