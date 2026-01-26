using UnityEngine;

public class CollectibleRotate : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Y axis
    public float rotationSpeed = 90f; // degrees per second

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
