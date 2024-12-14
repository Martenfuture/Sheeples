using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private float targetRotation = -90f;

    void Update()
    {
        float currentRotation = transform.eulerAngles.x;
        float newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(newRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }

}
