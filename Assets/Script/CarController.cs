using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WheelData
{
    public WheelCollider collider;
    public Transform mesh;
    public bool isSteerable;
    public bool isMotorized;
}

public class CarController : MonoBehaviour
{
    [Header("Settings")]
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    [Header("Wheels")]
    public List<WheelData> wheels;

    private float horizontalInput;
    private float verticalInput;
    private bool isBraking;

    void Update()
    {
        GetInput();
        UpdateWheels();
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // A, D or Left, Right
        verticalInput = Input.GetAxis("Vertical");     // W, S or Up, Down
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        float currentBrakeForce = isBraking ? brakeForce : 0f;

        foreach (var wheel in wheels)
        {
            if (wheel.isMotorized)
            {
                wheel.collider.motorTorque = verticalInput * motorForce;
            }
            wheel.collider.brakeTorque = currentBrakeForce;
        }
    }

    private void HandleSteering()
    {
        float steerAngle = horizontalInput * maxSteerAngle;
        foreach (var wheel in wheels)
        {
            if (wheel.isSteerable)
            {
                wheel.collider.steerAngle = steerAngle;
            }
        }
    }

    private void UpdateWheels()
    {
        foreach (var wheel in wheels)
        {
            Vector3 pos;
            Quaternion rot;
            // This grabs the physics position/rotation from the collider
            wheel.collider.GetWorldPose(out pos, out rot);

            // This applies it to your 3D mesh
            wheel.mesh.position = pos;
            wheel.mesh.rotation = rot;
        }
    }
}