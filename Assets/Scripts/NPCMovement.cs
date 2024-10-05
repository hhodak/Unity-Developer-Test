using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    NavMeshAgent agent;
    bool hasDestination = false;
    float planeSize = 25f;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDestination)
        {
            FindNewLocation();
            Move(position);
        }
        else
        {
            CheckIfArrivedToDestination();
        }
    }

    private void FindNewLocation()
    {
        bool hasPath;
        do
        {
            float xRandom = Random.Range(-planeSize, planeSize);
            float zRandom = Random.Range(-planeSize, planeSize);

            position = new Vector3(xRandom, transform.position.y, zRandom);
            Debug.Log(position.ToString());
            agent.SetDestination(position);

            hasPath = agent.CalculatePath(position, agent.path);

        } while (hasPath);
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
