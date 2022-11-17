using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SheepControllerBase : MonoBehaviour
{
    public Vector2 RandomRangeForward;

    public Vector3 WalkingDirection;

    public Dictionary<int, GameObject> SheepsNearby = new Dictionary<int, GameObject>();

    private bool isWalking;

    private void Start()
    {
        WalkingDirection = transform.forward;
    }

    private void Update()
    {
        if (!isWalking)
        {
            RunFront();
        }
        Debug.DrawRay(transform.position, WalkingDirection, Color.red);
    }
    public void RunFront()
    {
        Vector3 forwardPosition = transform.position - WalkingDirection * Random.Range(RandomRangeForward.x,RandomRangeForward.y);
        GetComponent<NavMeshAgent>().SetDestination(forwardPosition);
    }
    public void RunAway(GameObject other)
    {
        //Vector3 forwardPosition = transform.position - (other.transform.position - transform.position) * Random.Range(RandomRangeForward.x, RandomRangeForward.y);
        WalkingDirection = other.transform.position - transform.position;
        //GetComponent<NavMeshAgent>().SetDestination(forwardPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!SheepsNearby.ContainsKey(other.GetInstanceID()))   SheepsNearby.Add(other.GetInstanceID(),other.gameObject);
        Debug.Log(SheepsNearby.Count);
    }

    private void OnTriggerExit(Collider other)
    {
        SheepsNearby.Remove(other.GetInstanceID());
        Debug.Log(SheepsNearby.Count);
    }

    private void getAverageDirection()
    {
        Vector3 averageDirection = new Vector3();
        foreach (GameObject sheep in SheepsNearby.Values)
        {
            averageDirection += sheep.transform.forward;
        }
        averageDirection /= SheepsNearby.Count;
    }

    IEnumerator walkSleep()
    {
        isWalking = true;
        yield return new WaitForSeconds(5);
        RunFront();
        isWalking = false;
    }
}
