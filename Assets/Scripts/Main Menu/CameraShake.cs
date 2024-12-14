using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cityCamera;
    public float moveCamera;
    public float shake;
    public float yPosition;
    public float maxY;
    public float minY;
    public float xPosition;
    public float maxX;
    public float minX;
    public Transform objCityCamera;

    // Use this for initialization
    void Start()
    {
        yPosition = Random.Range(maxY, minY);
        xPosition = Random.Range(maxX, minX);
    }

    // Update is called once per frame
    void Update()
    {
        Reset();

        moveCamera += Time.deltaTime;
        cityCamera = objCityCamera.position;
        cityCamera.y = yPosition * Mathf.Sin(moveCamera * shake * Mathf.PI);
        cityCamera.x = xPosition * Mathf.Sin(moveCamera * shake * Mathf.PI);
        objCityCamera.position = cityCamera;
    }

    void Reset()
    {

    }
}

