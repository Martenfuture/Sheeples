using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SheepControllerBase : MonoBehaviour
{
    public Vector2 RandomRangeForward;
    public void RunFront()
    {
        Vector3 forwardPosition = transform.forward * Random.Range(RandomRangeForward.x,RandomRangeForward.y);
        GetComponent<NavMeshAgent>().SetDestination(forwardPosition);
    }
    public void RunAway(GameObject other)
    {
        Vector3 forwardPosition = transform.position - (other.transform.position - transform.position) * Random.Range(RandomRangeForward.x, RandomRangeForward.y);
        GetComponent<NavMeshAgent>().SetDestination(forwardPosition);
    }
}
