using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SheepManager : MonoBehaviour
{
    public static SheepManager instance = null;

    public GameObject agentPrefab;
    List<SheepGroup> sheepGroups = new List<SheepGroup>();
    public float forwardPostionMultiplier;

    public Vector3 targetPosition = Vector3.zero;
    Vector3 targetDirection = Vector3.zero;
    public float targetDirectionStrength = 3f;

    Vector3 middlePosition;

    [Range(1, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.5f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 5f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sheepGroups.Add(new SheepGroup());
        for (int i = 0; i < startingCount; i++)
        {
            Vector2 randomCirclePosition = Random.insideUnitCircle * startingCount * AgentDensity;
            Vector3 spawnPosition = new Vector3(transform.position.x + randomCirclePosition.x, transform.position.y, transform.position.z + randomCirclePosition.y);
            GameObject newAgent = Instantiate(
                agentPrefab,
                spawnPosition,
                Quaternion.Euler(Vector3.up * Random.Range(0, 0)),
                transform
                );
            newAgent.name = "Agent " + i;
            sheepGroups[0].sheeps.Add(newAgent);
        }
    }

    void Update()
    {
        foreach(SheepGroup sheepGroup in sheepGroups)
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
            direction += sheepGroup.targetDirection * targetDirectionStrength;
            direction.Normalize();
            sheepGroup.middlePosition /= sheepGroup.sheeps.Count;
            targetPosition = sheepGroup.middlePosition + direction * forwardPostionMultiplier;

        
            foreach (GameObject sheep in sheepGroup.sheeps)
            {
                sheep.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
            }
            Debug.DrawRay(targetPosition, transform.up * 10, Color.green);
        }
    }

    public void SetTargetDirection(Vector3 dogPostion, int sheepGroupId)
    {
        Vector3 direction = sheepGroups[sheepGroupId].middlePosition - dogPostion;
        direction.Normalize();
        sheepGroups[sheepGroupId].targetDirection = direction;
    }


    public void SplitSheepListRandom()
    {
        GameObject randomSheep = sheepGroups[0].sheeps[Random.Range(0, sheepGroups[0].sheeps.Count)];

        List<GameObject> newSheepGroup = sheepGroups[0].sheeps.Where(sheep => Vector3.Distance(sheep.transform.position, randomSheep.transform.position) < Random.Range(1,5)).ToList();
        sheepGroups[0].sheeps.RemoveAll(sheep => newSheepGroup.Contains(sheep));

        foreach (GameObject sheep in newSheepGroup)
        {
            sheep.GetComponent<SheepAgent>().sheepGroupId = sheepGroups.Count;
        }
        sheepGroups.Add(new SheepGroup() { sheeps = newSheepGroup });
    }
}
