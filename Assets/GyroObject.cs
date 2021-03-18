using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroObject : MonoBehaviour
{
    private const float ObjectSpeed = 15.0f;
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
         * Press rest button to fix issues with gyro.
         */
        float initialOrientationX = Input.gyro.rotationRateUnbiased.x;
        float initialOrientationY = Input.gyro.rotationRateUnbiased.y;
        _rb.AddForce(initialOrientationY * ObjectSpeed, 0.0f, -initialOrientationX * ObjectSpeed);
    }
}