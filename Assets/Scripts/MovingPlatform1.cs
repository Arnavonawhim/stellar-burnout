using UnityEngine;

public class MovingPlatform3D : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 2f;

    private Vector3 targetPos;

    void Start()
    {
        targetPos = pointB.position;
    }

    void Update()
    {
        // Move platform toward target point
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // Switch direction when reached
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (targetPos == pointB.position)
                targetPos = pointA.position;
            else
                targetPos = pointB.position;
        }
    }
}
