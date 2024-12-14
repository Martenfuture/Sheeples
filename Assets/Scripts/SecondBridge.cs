using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBridge : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private float targetRotation = -270f;

    void Update()
    {
        float currentRotation = transform.eulerAngles.x;
        float newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(newRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

}
