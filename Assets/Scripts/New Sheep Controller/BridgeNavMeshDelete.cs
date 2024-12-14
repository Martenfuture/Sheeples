using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeNavMeshDelete : MonoBehaviour
{
    public List<GameObject> setActive;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in setActive)
        {
            obj.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
