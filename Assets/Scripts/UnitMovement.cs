using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : Unit
{
    public void Move(Vector3 position)
    {
        hasDestination = true;
        agent.stoppingDistance = 0;
        agent.SetDestination(position);
    }

    public void CheckIfArrivedToDestination()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0 && hasDestination)
        {
            hasDestination = false;
        }
    }
}
