using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // Drag your McLaren here
    public Vector3 offset = new Vector3(0, 2, -5); // Default height and distance
    public float speed = 5f;      // How smoothly the camera follows

    private Rigidbody playerRB;

    void Start()
    {
        if (player != null)
        {
            playerRB = player.GetComponent<Rigidbody>();
        }
    }

    void LateUpdate()
    {
        if (player == null || playerRB == null) return;

        // Calculate the direction the car is moving or facing
        Vector3 playerForward = (playerRB.velocity.normalized + player.transform.forward).normalized;

        // Determine the target position based on the car's position, rotation, and your offset
        Vector3 targetPosition = player.position + player.transform.TransformVector(offset);

        // Smoothly move the camera to that target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // Always keep the camera looking at the car
        transform.LookAt(player);
    }
}