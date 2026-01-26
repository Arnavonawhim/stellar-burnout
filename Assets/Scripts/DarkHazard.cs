using UnityEngine;

public class DarkHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DarkPlayer"))
        {
            Destroy(other.gameObject);
        }
    }
}
