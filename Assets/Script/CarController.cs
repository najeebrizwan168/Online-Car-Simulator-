using Fusion;
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

public class CarController : NetworkBehaviour
{
    [Header("Settings")]
    public float motorForce = 2000f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    [Header("Wheels")]
    public List<WheelData> wheels;

    // Use Networked properties so everyone stays in sync
    [Networked] private float horizontalInput { get; set; }
    [Networked] private float verticalInput { get; set; }
    [Networked] private NetworkBool isBraking { get; set; }

    public override void Spawned()
    {
        // Automatically tell the camera to follow THIS car if it belongs to us
        if (HasInputAuthority)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            if (cam != null) cam.SetTarget(this.transform);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasInputAuthority)
        {
            // Gather input every network tick
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            isBraking = Input.GetKey(KeyCode.Space);
        }

        // Apply physics in the network loop (fixes the jitter)
        ApplyMotor();
        ApplySteering();

        // Update wheel visual rotation/position
        UpdateWheelVisuals();
    }

    private void ApplyMotor()
    {
        float brake = isBraking ? brakeForce : 0f;
        foreach (var wheel in wheels)
        {
            if (wheel.isMotorized) wheel.collider.motorTorque = verticalInput * motorForce;
            wheel.collider.brakeTorque = brake;
        }
    }

    private void ApplySteering()
    {
        float steer = horizontalInput * maxSteerAngle;
        foreach (var wheel in wheels)
        {
            if (wheel.isSteerable) wheel.collider.steerAngle = steer;
        }
    }

    private void UpdateWheelVisuals()
    {
        foreach (var wheel in wheels)
        {
            Vector3 pos;
            Quaternion rot;
            wheel.collider.GetWorldPose(out pos, out rot);
            wheel.mesh.position = pos;
            wheel.mesh.rotation = rot;
        }
    }
}