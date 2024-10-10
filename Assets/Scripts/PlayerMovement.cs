using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : UnitAction
{
    bool receivedInput = false;

    private void Update()
    {
        attackCooldown-=Time.deltaTime;
        if (!receivedInput)
        {
            if (hasDestination)
            {
                if (target == null)
                    CheckIfArrivedToDestination();
                else
                    CheckIfTargetInRange();
            }
            receivedInput = false;
        }
    }

    public void MoveToLocation(Vector3 location)
    {
        receivedInput = true;
        Move(location);
    }

    public void AttackTarget(Transform enemy)
    {
        receivedInput = true;
        Attack(enemy);
    }
}
