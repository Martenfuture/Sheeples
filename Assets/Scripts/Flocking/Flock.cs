using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 20000)]
    public int startingCount = 250;
    const float AgentDensity = 0.1f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 5f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        for (int i = 0; i < startingCount; i++)
        {
            Vector2 _randomCirclePosition = Random.insideUnitCircle * startingCount * AgentDensity;
            Vector3 _spawnPosition = new Vector3(transform.position.x + _randomCirclePosition.x,transform.position.y, transform.position.z + _randomCirclePosition.y);
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                _spawnPosition,
                Quaternion.Euler(Vector3.up * Random.Range(0, 360)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //DEBUG
            //Debug.DrawRay(agent.transform.position, agent.transform.up * 10, Color.Lerp(Color.green, Color.red, context.Count / 6f));

            Vector3 move = behavior.CalculateMove(agent, context, this);
            //Debug.DrawRay(agent.transform.position + move, agent.transform.up, Color.red);
            //Debug.DrawRay(move, agent.transform.up * 10, Color.green);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * agent.GetComponent<FlockAgent>().Speed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
