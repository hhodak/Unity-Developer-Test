using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hit;
    bool hasDestination = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.transform.tag)
                {
                    case "Ground":
                        Move(hit.point);
                        break;
                    case "NPC":
                        Debug.Log("Attack");
                        //attack
                        break;
                    default: break;
                }
            }
        }
        if(!hasDestination)
        {
            CheckIfArrivedToDestination();
        }
    }

    private void Move(Vector3 position)
    {
        hasDestination = true;
        agent.stoppingDistance = 0;
        //agent.isStopped = false;
        agent.SetDestination(position);
    }

    private void CheckIfArrivedToDestination()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0 && hasDestination)
        {
            hasDestination = false;
        }
    }

    private void Attack(Transform target)
    {
        //provjeri je li u dometu
        //ako je:
        ////napadni
        //ako nije:
        ////kreni prema lokaciji
    }
}
