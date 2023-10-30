using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingObjectVisual : MonoBehaviour
{
    [SerializeField] private float distance = 1.0f;
    [SerializeField] private Camera arCamera;

    private void Start()
    {
        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure to assign it or tag it as 'MainCamera'.");
        }
    }

    private void Update()
    {
        if (arCamera != null)
        {
            // Calculate the target position with the specified distance offset
            Vector3 targetPosition = arCamera.transform.position + (arCamera.transform.rotation * Vector3.forward * distance);

            // Make the object face the AR camera
            // transform.LookAt(Camera.main.transform.position, Vector3.up);
            transform.forward = arCamera.transform.forward;

            // Set the object's position to the target position
            transform.position = targetPosition;
        }
    }
}
