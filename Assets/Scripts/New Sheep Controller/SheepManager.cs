using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SheepManager : MonoBehaviour
{
    public static SheepManager instance = null;

    public GameObject agentPrefab;
    List<SheepGroup> sheepGroups = new List<SheepGroup>();
    public float forwardPostionMultiplier;
    public float baseSpeed;

    public Texture[] sheepTextures;
    public float[] sheepTexturesWeight;

    public List<Transform> spanwPositions;
    public List<int> spawnCount;

    public Vector3 targetPosition = Vector3.zero;
    Vector3 targetDirection = Vector3.zero;
    public float targetDirectionStrengthMultiplier = 0.25f;

    Vector3 middlePosition;

    const float AgentDensity = 0.5f;

    bool stuckCheck;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sheepTexturesWeight = SheepCore.WeightArrayPercent(sheepTexturesWeight);
        for (int s = 0; s < spanwPositions.Count; s++)
        {
            List<GameObject> newSheepGroup =  new List<GameObject>();
            int startingCount = spawnCount[s];
            for (int i = 0; i < startingCount; i++)
            {
                Vector2 randomCirclePosition = Random.insideUnitCircle * startingCount * AgentDensity;
                Vector3 spawnPosition = new Vector3(spanwPositions[s].position.x + randomCirclePosition.x, spanwPositions[s].position.y, spanwPositions[s].position.z + randomCirclePosition.y);
                GameObject newAgent = Instantiate(
                    agentPrefab,
                    spawnPosition,
                    Quaternion.Euler(Vector3.up * Random.Range(0, 0)),
                    transform
                    );
                newAgent.name = "Agent " + i;
                newSheepGroup.Add(newAgent);
                newAgent.GetComponent<NavMeshAgent>().speed = baseSpeed + Random.Range(-0.5f, 0.5f);
                newAgent.GetComponent<SheepAgent>().sheepMeshObject.GetComponent<Renderer>().materials[0].SetTexture("_BaseMap", sheepTextures[SheepCore.RandomWeightArrayIndex(sheepTexturesWeight)]);
                newAgent.GetComponent<SheepAgent>().sheepGroupId = s;
            }
            sheepGroups.Add(new SheepGroup() { sheeps = newSheepGroup });
        }
    }

    void Update()
    {
        if (!stuckCheck)
        {
            StartCoroutine(SheepStuckCheck());
        }

        foreach(SheepGroup sheepGroup in sheepGroups)
        {
            targetPosition = CalculateTargetPosition(sheepGroup);

        
            foreach (GameObject sheep in sheepGroup.sheeps)
            {
                if (!sheepGroup.insideFinishArea)
                {
                    sheep.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
                }
            }
            Debug.DrawRay(targetPosition, transform.up * 10, Color.green);
        }
    }

    private Vector3 CalculateTargetPosition(SheepGroup sheepGroup)
    {
        targetPosition = Vector3.zero;
        Vector3 direction = Vector3.zero;
        foreach (GameObject sheep in sheepGroup.sheeps)
        {
            direction += sheep.transform.forward;
        }
        direction /= sheepGroup.sheeps.Count;
        direction.Normalize();

        sheepGroup.middlePosition = Vector3.zero;
        foreach (GameObject sheep in sheepGroup.sheeps)
        {
            sheepGroup.middlePosition += sheep.transform.position;
        }
        sheepGroup.targetDirectionStrength += targetDirectionStrengthMultiplier * Time.deltaTime;
        direction += sheepGroup.targetDirection * Mathf.Lerp(3f, 0, sheepGroup.targetDirectionStrength);
        direction.Normalize();
        sheepGroup.middlePosition /= sheepGroup.sheeps.Count;
        targetPosition = sheepGroup.middlePosition + direction * forwardPostionMultiplier;

        return targetPosition;
    }

    public void SetTargetDirection(Vector3 dogPostion, int sheepGroupId, bool randomRotation)
    {
        //Debug.Log("Sheep Group Count: " + sheepGroups.Count + " / Sheep Group ID: " + sheepGroupId);
        if (sheepGroups[sheepGroupId] == null)
        {
            Debug.LogError("Sheep Group out of Range" + sheepGroupId);
            return;
        }
        Vector3 direction = sheepGroups[sheepGroupId].middlePosition - dogPostion;
        direction.Normalize();
        if (randomRotation)
        {
            float randomAngle = Random.Range(-60f, 60f);
            direction = Quaternion.Euler(0, randomAngle, 0) * direction;
        }
        sheepGroups[sheepGroupId].targetDirection = direction;
        sheepGroups[sheepGroupId].targetDirectionStrength = 0;
    }

    // Split sheep from main group if bool SplitGroup is true it splits a group, if not only one
    public void SplitSheepListRandom(bool SplitGroup)
    {
        GameObject randomSheep = sheepGroups[0].sheeps[Random.Range(0, sheepGroups[0].sheeps.Count)];
        float groupBaseSpeed = Random.Range(2, 3.5f);
        List<GameObject> newSheepGroup = new List<GameObject>();
        if (SplitGroup)
        {
            newSheepGroup = sheepGroups[0].sheeps.Where(sheep => Vector3.Distance(sheep.transform.position, randomSheep.transform.position) < Random.Range(1, 5)).ToList();
        }
        else
        {
            StartCoroutine(StopSheepDelay(randomSheep));
            newSheepGroup.Add(randomSheep);
            groupBaseSpeed = Random.Range(1, 2.5f);
        }
        sheepGroups[0].sheeps.RemoveAll(sheep => newSheepGroup.Contains(sheep));

        foreach (GameObject sheep in newSheepGroup)
        {
            // Effect at split on gameobject
            sheep.GetComponent<SheepAgent>().sheepGroupId = sheepGroups.Count;
            sheep.GetComponent<NavMeshAgent>().speed = groupBaseSpeed + Random.Range(-0.5f, 0.5f);
        }
        sheepGroups.Add(new SheepGroup() { sheeps = newSheepGroup });
    }

    // Merges group to main group by its int groupindex
    public void MergeSheepGroup(int groupIndex)
    {
        Debug.Log("Merge Group Index: " + groupIndex);
        if (sheepGroups.Count == 1 || groupIndex == 0)
        {
            return;
        }
        foreach (GameObject sheep in sheepGroups[groupIndex].sheeps)
        {
            // Effect at merge on gameobject
            sheepGroups[0].sheeps.Add(sheep);
            sheep.GetComponent<NavMeshAgent>().speed = baseSpeed + Random.Range(-0.5f, 0.5f);
            sheep.GetComponent<SheepAgent>().sheepGroupId = 0;
        }
        for (int i = sheepGroups.Count - 1; i > groupIndex; i--)
        {
            foreach (GameObject sheep in sheepGroups[i].sheeps)
            {
                sheep.GetComponent<SheepAgent>().sheepGroupId--;
            }
        }
        sheepGroups.RemoveAt(groupIndex);
    }

    public void EnterFinishArea(int sheepGroupId, GameObject finishArea)
    {
        if (!sheepGroups[sheepGroupId].insideFinishArea)
        {
            sheepGroups[sheepGroupId].insideFinishArea = true;
            foreach (GameObject sheep in sheepGroups[sheepGroupId].sheeps)
            {
                sheep.GetComponent<NavMeshAgent>().speed *= 0.25f;
            }
            StartCoroutine(SheepInsideFinishArea(sheepGroupId, finishArea));
        }
    }

    IEnumerator SheepInsideFinishArea(int sheepGroupId, GameObject finishArea)
    {

        foreach (GameObject sheep in sheepGroups[sheepGroupId].sheeps)
        {
            Vector2 randomCirclePosition = Random.insideUnitCircle * 5;
            Vector3 walkPosition = new Vector3(finishArea.transform.position.x + randomCirclePosition.x, finishArea.transform.position.y, finishArea.transform.position.z + randomCirclePosition.y);
            sheep.GetComponent<NavMeshAgent>().SetDestination(walkPosition);
        }
        yield return new WaitForSeconds(2);

        StartCoroutine(SheepInsideFinishArea(sheepGroupId, finishArea));
    }

    IEnumerator StopSheepDelay(GameObject sheep)
    {
        yield return new WaitForSeconds(1);
        SetTargetDirection(sheepGroups[0].middlePosition, sheep.GetComponent<SheepAgent>().sheepGroupId, false);
        yield return new WaitForSeconds(Random.Range(6,10));
        sheep.GetComponent<NavMeshAgent>().speed = 0;
    }

    // Stecks if the sheeps cant reach the destination or have been in the same distance, runs every 2 seconds
    IEnumerator SheepStuckCheck()
    {
        stuckCheck = true;

        foreach (SheepGroup sheepGroup in sheepGroups)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 sheepGroupTargetPosition = CalculateTargetPosition(sheepGroup);
            if (!NavMesh.CalculatePath(sheepGroup.middlePosition, sheepGroupTargetPosition, NavMesh.AllAreas, path))
            {
                //Debug.Log("NOT REACHABLE: " + sheepGroup.sheeps[0].GetComponent<SheepAgent>().sheepGroupId);
                SetTargetDirection(sheepGroupTargetPosition, sheepGroup.sheeps[0].GetComponent<SheepAgent>().sheepGroupId, true);
            }
            else if (Vector3.Distance(sheepGroup.middlePosition, sheepGroup.lastPosition) < 1)
            {
                SetTargetDirection(sheepGroupTargetPosition, sheepGroup.sheeps[0].GetComponent<SheepAgent>().sheepGroupId, true);
            }
            sheepGroup.lastPosition = sheepGroup.middlePosition;
        }

        yield return new WaitForSeconds(2);
        stuckCheck = false;

    }


    ////////////DEBUG//////////////////

    public void DebugPathCheck()
    {
        foreach (SheepGroup sheepGroup in sheepGroups)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 sheepGroupTargetPosition = CalculateTargetPosition(sheepGroup);
            if (!NavMesh.CalculatePath(sheepGroup.middlePosition, sheepGroupTargetPosition, NavMesh.AllAreas, path))
            {
                Debug.Log("NOT REACHABLE: " + sheepGroup.sheeps[0].GetComponent<SheepAgent>().sheepGroupId);
            } 
            else
            {
                Debug.Log("REACHABLE: " + sheepGroup.sheeps[0].GetComponent<SheepAgent>().sheepGroupId);
            }
        }
    }
}
