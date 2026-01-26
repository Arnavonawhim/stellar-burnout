using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lightTarget;
    public Transform darkTarget;

    public Vector3 offset = new Vector3(0, 2, -10);
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        Transform target = null;

        if (lightTarget && lightTarget.gameObject.activeInHierarchy)
            target = lightTarget;
        else if (darkTarget && darkTarget.gameObject.activeInHierarchy)
            target = darkTarget;

        if (!target) return;

        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;

        transform.rotation = Quaternion.identity;
    }
}
