using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    public float speed = 1f;
   

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speed);
    }

    
}
