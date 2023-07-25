using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    [SerializeField] private float minX; // Minimum x position for the camera
    [SerializeField] private float maxX; // Maximum x position for the camera
    [SerializeField] private float minY; // Minimum y position for the camera
    [SerializeField] private float maxY; // Maximum y position for the camera
    
    void Update()
    {
        Vector3 targetPosition = target.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
