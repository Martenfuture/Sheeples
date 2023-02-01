using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBridge : MonoBehaviour
{



    public GameObject Manager1;
    public GameObject Manager2;

    MoveBridge script1;
    SecondBridge script2;

    // Start is called before the first frame update
    void Start()
    {
        script1 = Manager1.GetComponent<MoveBridge>();
        script2 = Manager2.GetComponent<SecondBridge>();
    }

    // Update is called once per frame
    private void OnTriggerEnter()
    {
        script1.enabled = true;
        script2.enabled = true;
    }
}
