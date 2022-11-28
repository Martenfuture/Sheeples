using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepManager : MonoBehaviour
{
    public static SheepManager instance = null;

    public GameObject agentPrefab;
    List<GameObject> agents = new List<GameObject>();
    public float forwardPostionMultiplier;

    public Vector3 targetPosition = Vector3.zero;
    public Vector3 targetDirection = Vector3.zero;
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
            agents.Add(newAgent);
        }
    }

    void Update()
    {
        targetPosition = Vector3.zero;
        Vector3 direction = Vector3.zero;
        foreach (GameObject agent in agents)
        {
            direction += agent.transform.forward;
        }
        direction /= agents.Count;
        direction.Normalize();

        middlePosition = Vector3.zero;
        foreach (GameObject agent in agents)
        {
            middlePosition += agent.transform.position;
        }
        direction += targetDirection * targetDirectionStrength;
        direction.Normalize();
        Debug.DrawRay(middlePosition, targetDirection * 10, Color.red);
        middlePosition /= agents.Count;
        targetPosition = middlePosition + direction * forwardPostionMultiplier;

        
        foreach (GameObject agent in agents)
        {
            agent.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
        }
        Debug.DrawRay(targetPosition, transform.up * 10, Color.green);
    }

    public void SetTargetDirection(Vector3 DogPostion)
    {
        targetDirection = middlePosition - DogPostion;
        targetDirection.Normalize();
    }
}
