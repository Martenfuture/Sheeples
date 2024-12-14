using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestClearBridge : MonoBehaviour
{
    public int questId;
    int collisionCount = 1;

    private void FixedUpdate()
    {
        if (collisionCount == 0)
        {
            QuestSimpleManager.instance.CompleteQuest(questId);
            gameObject.SetActive(false);
        }
        collisionCount = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "AttachableObject")
        {
            collisionCount++;
        }
    }

}
