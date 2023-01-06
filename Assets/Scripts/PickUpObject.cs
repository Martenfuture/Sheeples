using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    GameObject attachtedObject;
    public List<GameObject> nearObjects;
    GameObject nearestObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttachableObject")
        {
            nearObjects.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "AttachableObject")
        {
            nearObjects.Remove(other.gameObject);
        }
    }

    public void InteractWithObject(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            Debug.Log("Interact with Object:" + nearObjects.Count);
            if (attachtedObject == null)
            {
                if (nearObjects.Count > 0)
                {
                    GetNearestObject();
                    attachtedObject = nearestObject;
                    nearestObject.transform.SetParent(gameObject.transform, false);
                    nearestObject.transform.localPosition = new Vector3(0, 0.723999977f, 1.90100002f);
                    nearestObject.GetComponent<Collider>().enabled = false;
                    nearObjects.Remove(nearestObject);
                }
            }
            else
            {
                nearestObject.transform.SetParent(null, true);
                nearestObject.GetComponent<Collider>().enabled = true;
                attachtedObject = null;
            }
        }
    }


    public void GetNearestObject()
    {
        if (nearObjects.Count > 0)
        {
            GameObject obj = nearObjects[0];

            foreach (GameObject go in nearObjects)
            {
                if (Vector3.Distance(go.transform.position, gameObject.transform.position) < Vector3.Distance(obj.transform.position, gameObject.transform.position))
                {
                    obj = go;
                }
            }

            nearestObject = obj;
        }
    }
}
