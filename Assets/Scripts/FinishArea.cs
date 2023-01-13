using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheep")
        {
            SheepManager.instance.EnterFinishArea(other.GetComponent<SheepAgent>().sheepGroupId, gameObject);
        }
    }
}
