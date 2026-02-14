using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public float distance = 3f;      // Travel distance on X
    public float speed = 2f;         // Movement speed
    public float waitTime = 1f;      // Pause at each end
    public bool startRight = true;   // Initial direction

    Vector3 startPos;
    Vector3 targetPos;
    bool moving = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + new Vector3(distance, 0, 0);

        if (!startRight)
            (startPos, targetPos) = (targetPos, startPos);

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Move
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }

            // Swap end-points
            (startPos, targetPos) = (targetPos, startPos);

            // Wait time
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Stick player to platform
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.transform.SetParent(transform);
        }
    }

    // Unstick player when leaving
    void OnCollisionExit(Collision col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.transform.SetParent(null);
        }
    }
}
