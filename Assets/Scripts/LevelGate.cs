using UnityEngine;

public class DoorGate : MonoBehaviour
{
    public int requiredLight;
    public int requiredDark;

    public GameObject blocker; // door object to hide after completion

    void Start()
    {
        if (!blocker) blocker = gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightPlayer") || other.CompareTag("DarkPlayer"))
        {
            CheckRequirements();
        }
    }

    void CheckRequirements()
    {
        var cm = CollectibleManager.instance;

        if (cm.totalLight >= requiredLight && cm.totalDark >= requiredDark)
        {
            blocker.SetActive(false);
        }
        else
        {
            UICollectPrompt.instance.ShowPrompt(
                $"{requiredLight - cm.totalLight} Photons & {requiredDark - cm.totalDark} Antimatter needed"
            );
        }
    }
}
