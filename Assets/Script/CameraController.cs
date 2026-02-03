using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -7); // Adjusted for a better racing view
    public float smoothTime = 0.05f;

    private Vector3 currentVelocity = Vector3.zero;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Follow the car's visual position directly
        Vector3 targetPosition = target.position + target.TransformDirection(offset);

        // SmoothDamp is the key to stopping the jitter at high speeds
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Look at the car
        transform.LookAt(target.position + Vector3.up);
    }
}