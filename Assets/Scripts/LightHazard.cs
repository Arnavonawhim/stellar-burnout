using UnityEngine;

public class LightHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPlayer"))
        {
            Destroy(other.gameObject);
        }
    }
}
