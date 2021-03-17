using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroObject : MonoBehaviour
{
    private void Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        ApplyGyroRotation();
    }

    private void ApplyGyroRotation()
    {
        //Reference https://answers.unity.com/questions/970891/rotating-a-camera-using-the-gyroscope.html
        transform.Rotate(-Input.gyro.rotationRateUnbiased.x / 2, -Input.gyro.rotationRateUnbiased.y / 2, 0);
    }
}