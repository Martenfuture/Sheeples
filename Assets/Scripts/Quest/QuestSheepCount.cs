using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSheepCount : MonoBehaviour
{
    
    private TextMeshProUGUI textCounter;
    [SerializeField] QuestText questScriptableObject;

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

        textCounter.text = questScriptableObject.text + "0/" + SheepManager.instance.sheepCountTotal;
    }
}
