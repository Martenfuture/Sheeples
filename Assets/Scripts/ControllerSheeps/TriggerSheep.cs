using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSheep : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheep")
        {
            other.transform.parent.parent.GetComponent<SheepControllerBase>().RunAway(gameObject);
        }
    }
}
