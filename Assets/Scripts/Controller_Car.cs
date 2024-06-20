using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Car : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public ControlMode control;

    private float horizontalInput;
    private float verticalInput;

    private float currentbreakForce;
    private bool isBreaking;

    private float currentsteeringAngle;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    private void FixedUpdate()
    {
        GetUserInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    public void MoveInput(float input)
    {
        verticalInput = input;
    }
    private void GetUserInput()
    {
        if(control == ControlMode.Keyboard)
        {
            horizontalInput = Input.GetAxis("horizontal");
            verticalInput = Input.GetAxis("vertical");


            isBreaking = Input.GetKey(KeyCode.Space);
        }


    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentbreakForce = isBreaking ? breakForce : 0;

        if (isBreaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentsteeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentsteeringAngle;
        frontRightWheelCollider.steerAngle = currentsteeringAngle;

    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
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