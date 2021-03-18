using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroObject : MonoBehaviour
{
    private const float ObjectSpeed = 10.0f;
    private Rigidbody _rb;

    private void Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 120;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ApplyGyroRotation();
    }

    private void ApplyGyroRotation()
    {
        /*
         * Press reset button to fix issues with gyro.
         */
        float initialOrientationX = Input.gyro.rotationRateUnbiased.x;
        float initialOrientationZ = Input.gyro.rotationRateUnbiased.z;
        _rb.AddForce(-initialOrientationZ * ObjectSpeed, 0.0f, -initialOrientationX * ObjectSpeed);
    }
}