using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskTrigger : MonoBehaviour
{
    public int TaskTriggerID;

    public bool ActivateOnTriggerEnter;
    public bool ActivateOnCollisionEnter;

    public UnityEvent ExtraEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (ActivateOnTriggerEnter)
        {
            GameEvents.TaskTrigger.TaskTriggerEnter(TaskTriggerID, other.gameObject);
            ExtraEvents.Invoke();
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (ActivateOnCollisionEnter)
        {
            GameEvents.TaskTrigger.TaskTriggerEnter(TaskTriggerID, collision.gameObject);
            ExtraEvents.Invoke();
        }
    }
}
