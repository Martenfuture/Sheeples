using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Object Avoidance")]
public class ObjectAvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        //add all points together and average
        float lowestDistance = 100;
        Vector3 avoidanceMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (item.name == "Test_Cube") Debug.Log("COLLISION");
            if ((Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius) && (lowestDistance > Vector3.Distance(agent.transform.position, item.position)))
            {
                avoidanceMove = agent.transform.position - item.position;
                lowestDistance = Vector3.Distance(agent.transform.position, item.position);
            }
        }


        return avoidanceMove;
    }
}
