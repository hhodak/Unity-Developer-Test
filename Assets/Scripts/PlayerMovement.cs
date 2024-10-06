using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth = 10;
    private float damage = 2;
    private float range = 3f;

    NavMeshAgent agent;
    RaycastHit hit;
    bool hasDestination = false;
    bool inRange = false;
    Transform target = null;

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
                        Attack(hit.transform);
                        break;
                    default: break;
                }
            }
        }
        if (!hasDestination)
        {
            if (target == null)
                CheckIfArrivedToDestination();
            else
                CheckIfEnemyInRange();
        }
    }

    private void Move(Vector3 position)
    {
        hasDestination = true;
        agent.stoppingDistance = 0;
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

    private void CheckIfEnemyInRange()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.remainingDistance <= range && target != null)
        {
            inRange = true;
        }
    }

    private void Attack(Transform target)
    {
        this.target = target;
        hasDestination = true;
        agent.stoppingDistance = 1;
        agent.SetDestination(target.position);
        CheckIfEnemyInRange();
        if (inRange)
        {
            float remainingDamage = target.GetComponent<NPCMovement>().TakeDamage(damage);
            if (remainingDamage <= 0)
            {
                Destroy(target.gameObject);
                hasDestination = false;
                inRange = false;
            }
        }
        else
        {
            Move(target.position);
        }
    }
}
