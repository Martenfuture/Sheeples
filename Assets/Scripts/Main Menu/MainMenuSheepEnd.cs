using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSheepEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheep")
        {
            transform.parent.GetComponent<MainMenuSheepSpawner>().ReachEndArea(other.gameObject);
        }
    }
}
