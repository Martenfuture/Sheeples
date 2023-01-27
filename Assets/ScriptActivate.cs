using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivate : MonoBehaviour
{

    public GameObject Manager;
    MoveBridge script;



    // Start is called before the first frame update
    void Start()
    {

        script = Manager.GetComponent<MoveBridge>();

        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            script.enabled = true;
        }
    }


}
