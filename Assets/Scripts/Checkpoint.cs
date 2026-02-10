using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        // allow both characters
        if (other.CompareTag("LightPlayer") || other.CompareTag("DarkPlayer"))
        {
            // set checkpoint
            RespawnManager.instance.SetCheckpoint(transform.position);

            activated = true;

            // optional: visual feedback
            // e.g. change material color, particle, sound, UI, etc.
        }
    }
}
