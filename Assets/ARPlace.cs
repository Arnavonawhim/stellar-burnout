using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPlace : MonoBehaviour
{
    public GameObject objectToPlace;
    ARRaycastManager raycastManager;
    GameObject spawnedObject;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (spawnedObject != null) return;

        var touch = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(touch, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            spawnedObject = Instantiate(objectToPlace, pose.position, pose.rotation);
        }
    }
}
