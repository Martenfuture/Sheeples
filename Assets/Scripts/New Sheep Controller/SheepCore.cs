using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCore
{
    public static int RandomWeightArrayIndex(float[] actionWeight)
    {
        float randValue = Random.value; // Randome value (chance value)

        float currentChance = 0f;
        float minChanceRange;

        int returnValue = -1;

        for (int i = 0; i < actionWeight.Length; i++)
        {

            minChanceRange = currentChance;
            currentChance += actionWeight[i];

            if (randValue >= minChanceRange && randValue <= currentChance)
            {
                returnValue = i;
                break;
            }
        }

        return returnValue;
    }

    public static float[] WeightArrayPercent(float[] actionWeight)
    {

        float weightSum = 0;
        for (int i = 0; i < actionWeight.Length; i++)
        {
            weightSum += actionWeight[i];
        }
        for (int i = 0; i < actionWeight.Length; i++)
        {
            actionWeight[i] = actionWeight[i] * 100 / weightSum / 100;
        }

        return actionWeight;
    }
}
