using UnityEngine;
using UnityEngine.InputSystem;

public class DarkPowerController : MonoBehaviour
{
    public float activationRange = 2f;
    public Key destroyKey = Key.F;

    Keyboard kb;
    DarkDestructible nearestTarget;

    void Start()
    {
        kb = Keyboard.current;
    }

    void Update()
    {
        FindNearestDestructible();

        if (nearestTarget != null && kb[destroyKey].wasPressedThisFrame)
        {
            nearestTarget.ActivateDestroy();
        }
    }

    void FindNearestDestructible()
    {
        nearestTarget = null;
        float bestDist = activationRange;

        DarkDestructible[] objs = FindObjectsByType<DarkDestructible>(FindObjectsSortMode.None);
        foreach (var d in objs)
        {
            float dist = Vector3.Distance(transform.position, d.transform.position);
            if (dist < bestDist && !d.IsDestroyed)
            {
                bestDist = dist;
                nearestTarget = d;
            }
        }
    }
}
