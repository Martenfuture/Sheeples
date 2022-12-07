using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSheepManager : MonoBehaviour
{
    public float randomIntencity;

    [Tooltip("1: Split Group, 2: Split Single")]
    public float[] actionWeight;
    public Vector2 randomTimeRange;
    bool triggert;
    SheepManager Manager;

    private void Start()
    {
        Manager = GetComponent<SheepManager>();
        float weightSum = 0;
        for (int i = 0; i < actionWeight.Length; i++)
        {
            weightSum += actionWeight[i];
        }
        for (int i = 0; i < actionWeight.Length; i++)
        {
            actionWeight[i] = actionWeight[i] * 100 / weightSum / 100;
        }
    }
    private void Update()
    {
        if (!triggert)
        {
            triggert = true;
            StartCoroutine(TriggerRandomAction());
        }
    }

    IEnumerator TriggerRandomAction()
    {
        yield return new WaitForSeconds(Random.Range(randomTimeRange.x, randomTimeRange.y) * randomIntencity);

        float randValue = Random.value; // Randome value (chance value)

        float currentChance = 0f;
        float minChanceRange;

        for (int i = 0; i < actionWeight.Length; i++)
        {

            minChanceRange = currentChance;
            currentChance += actionWeight[i];

            if (randValue >= minChanceRange && randValue <= currentChance)
            {
                switch (i)
                {
                    case 0:
                        Manager.SplitSheepListRandom(true);
                        break;
                    case 1:
                        Manager.SplitSheepListRandom(false);
                        break;
                    default:
                        Debug.LogError("ReanomSheepManager: Action Weights Array to long");
                        break;
                }
            }
        }

        triggert = false;
    }
}
