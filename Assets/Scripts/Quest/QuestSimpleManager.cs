using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSimpleManager : MonoBehaviour
{
    public static QuestSimpleManager instance;
    [SerializeField] List<QuestText> questTexts = new List<QuestText>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questTexts[i].text;
        }
    }

    public void CompleteQuest(int questId)
    {
        TextMeshProUGUI text = transform.GetChild(questId).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        transform.GetChild(questId).transform.GetChild(1).gameObject.SetActive(true);
        text.fontStyle = FontStyles.Strikethrough;
        text.color = Color.grey;
    }
}
