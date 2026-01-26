using UnityEngine;
using UnityEngine.InputSystem;

public class DarkPullController : MonoBehaviour
{
    public CharacterSwitcher switcher;
    public Key pullKey = Key.Q;
    public float pullRadius = 2f;

    PullableObject currentObj;
    Keyboard kb;

    void Start()
    {
        kb = Keyboard.current;
    }

    void Update()
    {
        if (!switcher.IsDark())
        {
            if (currentObj != null) currentObj.StopPull();
            currentObj = null;
            return;
        }

        if (kb[pullKey].wasPressedThisFrame)
        {
            TryStartPull();
        }

        if (kb[pullKey].wasReleasedThisFrame)
        {
            StopPull();
        }
    }

    void TryStartPull()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pullRadius);

        foreach (var h in hits)
        {
            PullableObject po = h.GetComponent<PullableObject>();
            if (po != null)
            {
                Debug.Log("Found Pullable: " + h.name);
                currentObj = po;
                po.StartPull(transform);
                return;
            }
        }

        Debug.Log("No pullable objects in range");
    }

    void StopPull()
    {
        if (currentObj != null) currentObj.StopPull();
        currentObj = null;
    }
}
