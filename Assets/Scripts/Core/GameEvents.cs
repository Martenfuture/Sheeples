using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents TaskTrigger;
    private void Awake()
    {
        TaskTrigger = this;
    }

    public event Action<int, GameObject> onTaskTriggerEnter;
    public void TaskTriggerEnter(int id, GameObject collider)
    {
        if (onTaskTriggerEnter != null)
        {
            onTaskTriggerEnter(id, collider);
        }
    }
}

/* How to use

public int TaskTriggerID

private void Start()
{
    GameEvents.TaskTrigger.onTaskTriggerEnter += OnPlatformActivate;
}

private void OnPlatformActivate(int id, GameObject collider)
{
    if (id == TaskTriggerID)
    {
        
    }
}

 */
