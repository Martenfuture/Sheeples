using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepManager : MonoBehaviour
{
    public GameObject agentPrefab;
    List<GameObject> agents = new List<GameObject>();
    public float forwardPostionMultiplier;

    public Vector3 TargetPosition = Vector3.zero;

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
        TargetPosition = Vector3.zero;
        Vector3 direction = Vector3.zero;
        foreach (GameObject agent in agents)
        {
            direction += agent.transform.forward;
        }
        direction /= agents.Count;
        direction.Normalize();

        Vector3 middlePosition = Vector3.zero;
        foreach (GameObject agent in agents)
        {
            middlePosition += agent.transform.position;
        }
        middlePosition /= agents.Count;
        TargetPosition = middlePosition + direction * forwardPostionMultiplier;
        
        foreach (GameObject agent in agents)
        {
            agent.GetComponent<NavMeshAgent>().SetDestination(TargetPosition);
        }
        Debug.DrawRay(TargetPosition, transform.up * 10, Color.green);
    }
}
