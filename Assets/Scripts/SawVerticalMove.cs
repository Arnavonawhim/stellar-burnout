using UnityEngine;
using System.Collections;

public class SawVerticalCycle : MonoBehaviour
{
    public float distance = 4.5f;   // how far to move down
    public float speed = 1f;        // movement speed
    public float pauseTime = 1f;    // pause at top and bottom

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        Vector3 bottomPos = startPos - Vector3.up * distance;

        while (true)
        {
            // Move down
            yield return MoveTo(bottomPos);

            // pause at bottom
            yield return new WaitForSeconds(pauseTime);

            // Move up
            yield return MoveTo(startPos);

            // pause at top
            yield return new WaitForSeconds(pauseTime);
        }
    }

    IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
