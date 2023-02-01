using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBridge : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(-270, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
