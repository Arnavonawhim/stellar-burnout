using UnityEngine;
using UnityEngine.InputSystem;

public class SolarPowerController : MonoBehaviour
{
    public float activationRange = 2f;
    public Key activationKey = Key.Q;

    Keyboard kb;
    public Key growKey = Key.F;
    SolarGrow nearestGrowBlock;
    FlowerBloom nearestFlower;

    void Start()
    {
        kb = Keyboard.current;
    }

    void Update()
    {
        FindNearestFlower();

        if (nearestFlower != null && kb[activationKey].wasPressedThisFrame)
        {
            nearestFlower.ActivateBloom();
        }
        FindNearestGrowBlock();

        if (nearestGrowBlock != null && kb[growKey].wasPressedThisFrame)
        {
            nearestGrowBlock.StartGrowth();
        }

    }

    void FindNearestFlower()
    {
        nearestFlower = null;
        float bestDist = activationRange;

        FlowerBloom[] flowers = FindObjectsByType<FlowerBloom>(FindObjectsSortMode.None);
        foreach (var f in flowers)
        {
            float d = Vector3.Distance(transform.position, f.transform.position);
            if (d < bestDist && !f.IsBloomed)
            {
                bestDist = d;
                nearestFlower = f;
            }
        }
    }

    void FindNearestGrowBlock()
    {
        nearestGrowBlock = null;
        float bestDist = activationRange;

        SolarGrow[] blocks = FindObjectsByType<SolarGrow>(FindObjectsSortMode.None);
        foreach (var b in blocks)
        {
            float d = Vector3.Distance(transform.position, b.transform.position);
            if (d < bestDist && !b.IsMaxed)
            {
                bestDist = d;
                nearestGrowBlock = b;
            }
        }
    }

}
