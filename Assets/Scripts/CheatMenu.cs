using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            QuestSheepCount.instance.CompleteQuest();
            ManageScene.instance.ShowFinishUI();
            Timer.instance.TimerStarted = false;
        }
    }
}
