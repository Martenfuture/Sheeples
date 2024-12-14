using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    int sheepsInFinishArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sheep" && !SheepManager.instance.sheepGroups[other.GetComponent<SheepAgent>().sheepGroupId].insideFinishArea)
        {
            int groupId = other.GetComponent<SheepAgent>().sheepGroupId;
            SheepManager.instance.EnterFinishArea(groupId, gameObject);
            sheepsInFinishArea += SheepManager.instance.sheepGroups[groupId].sheeps.Count;
            QuestSheepCount.instance.SetCounter(sheepsInFinishArea);

            if (sheepsInFinishArea >= SheepManager.instance.sheepCountTotal)
            {
                QuestSheepCount.instance.CompleteQuest();
                ManageScene.instance.ShowFinishUI();
                Timer.instance.TimerStarted = false;
            }
        }
    }
}
