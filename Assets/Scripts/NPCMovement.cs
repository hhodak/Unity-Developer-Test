using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private Renderer renderer;
    [SerializeField] private float currentHealth;
    private float maxHealth = 10;

    NavMeshAgent agent;
    bool hasDestination = false;
    bool isAttacking = false;
    bool isAttacked = false;
    float planeSize = 25f;
    private Vector3 position;

    private void Awake()
    {
        currentHealth = maxHealth;
        renderer = healthBar.GetComponent<Renderer>();
        renderer.material.color = Color.green;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
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
            agent.SetDestination(position);

            hasPath = agent.CalculatePath(position, agent.path);

        } while (hasPath);
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

    private void RunAway(Transform attacker)
    {
        //run away from attacker
        Debug.Log("Running away...");
    }

    public float TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0)
        {
            float scaleY = healthBar.transform.localScale.y * currentHealth / maxHealth;
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x, scaleY, healthBar.transform.localScale.z);
            ChangeBarColor();
        }
        return currentHealth;
    }

    private void ChangeBarColor()
    {
        if (currentHealth / maxHealth < 0.33f)
        {
            renderer.material.color = Color.red;
        }
        else if (currentHealth / maxHealth < 0.66f)
        {
            renderer.material.color = Color.yellow;
        }
        else
        {
            renderer.material.color = Color.green;
        }
    }
}
