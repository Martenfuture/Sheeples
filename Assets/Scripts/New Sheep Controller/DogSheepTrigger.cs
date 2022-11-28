using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSheepTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sheep")
        {
            Debug.Log("Sheep Triggert");
            SheepManager.instance.SetTargetDirection(transform.position);
        }
    }
}
