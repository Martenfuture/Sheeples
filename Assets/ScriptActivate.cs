using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivate : MonoBehaviour
{

    public GameObject Br�ckeMitGel�nde_02(1);
    MoveBridge script;



    // Start is called before the first frame update
    void Start()
    {

        script = Br�ckeMitGel�nde_02.GetComponent<MoveBridge>();
        script.enabled = true;


    }

  
}
