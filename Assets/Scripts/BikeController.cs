using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    private float currentbreakForce;
    private bool isBreaking;

    private float currentsteeringAngle;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontWheelCollider;
    [SerializeField] private WheelCollider rearWheelCollider;

    [SerializeField] private Transform frontWheelTransform;
    [SerializeField] private Transform rearWheelTransform;

    private void FixedUpdate()
    {
        GetUserInput();     
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }

    private void GetUserInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        rearWheelCollider.motorTorque = verticalInput * motorForce;

        currentbreakForce = isBreaking ? breakForce : 0;

        if (isBreaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        frontWheelCollider.brakeTorque = currentbreakForce;
        rearWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentsteeringAngle = maxSteeringAngle * horizontalInput;
        frontWheelCollider.steerAngle = currentsteeringAngle;

    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontWheelCollider, frontWheelTransform);
        UpdateSingleWheel(rearWheelCollider, rearWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        WheelCollider.GetWorldPose(out position, out rotation);
        WheelTransform.rotation = rotation;
        WheelTransform.position = position;
    }

}
