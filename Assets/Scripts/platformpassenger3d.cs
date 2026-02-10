using UnityEngine;

public class PlatformPassenger3D : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DarkPlayer"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("DarkPlayer"))
        {
            collision.transform.SetParent(null);
        }
    }
}
