using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : UnitAction
{
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (!hasDestination)
        {
            FindNewLocation();
            Move(position);
        }
        else
        {
            if (target == null)
            {
                CheckIfArrivedToDestination();
            }
            else
            {
                CheckIfTargetInRange();
            }
        }
    }
}
