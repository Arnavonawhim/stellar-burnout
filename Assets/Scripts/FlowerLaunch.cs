using UnityEngine;

public class FlowerLaunch : MonoBehaviour
{
    public float launchForce = 40f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPlayer"))
        {
            var controller = other.GetComponent<CharacterAnimationController>();
            if (controller) controller.Launch(launchForce);
        }
    }
}
