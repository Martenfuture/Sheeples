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
        actionWeight = SheepCore.WeightArrayPercent(actionWeight);
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

        int randomActionIndex = SheepCore.RandomWeightArrayIndex(actionWeight);
        switch (randomActionIndex)
        {
            case 0:
                Manager.SplitSheepListRandom(true);
                break;
            case 1:
                Manager.SplitSheepListRandom(false);
                break;
            case -1:
                Debug.LogError("ReanomSheepManager: Action Weights Array to long");
                break;
            default:
                Debug.LogError("ReanomSheepManager: Action Weights Array to long");
                break;
        }

        triggert = false;
    }
}
