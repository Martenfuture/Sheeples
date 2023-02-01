using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestSheepCount : MonoBehaviour
{
    
    private TextMeshProUGUI textCounter;
    [SerializeField] QuestText questScriptableObject;
    int sheepCounter;

    public static QuestSheepCount instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        textCounter = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        StartCoroutine(SetStartText());
    }

    public void SetCounter(int count)
    {
        sheepCounter = count;
        textCounter.text = questScriptableObject.text + count + "/" + SheepManager.instance.sheepCountTotal;
    }

    public void CompleteQuest()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        textCounter.fontStyle = FontStyles.Strikethrough;
        textCounter.color = Color.grey;
    }

    IEnumerator SetStartText()
    {
        yield return new WaitForEndOfFrame();

        textCounter.text = questScriptableObject.text + sheepCounter + "/" + SheepManager.instance.sheepCountTotal;
    }
}
