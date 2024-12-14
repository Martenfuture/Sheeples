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

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ManageScene.instance.PlayLevelId(3);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ManageScene.instance.PlayLevelId(4);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ManageScene.instance.PlayLevelId(5);
        }
    }
}
