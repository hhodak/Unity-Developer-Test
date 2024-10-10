using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitAction : UnitMovement
{
    public Vector3 FindNewLocation()
    {
        bool hasPath;
        do
        {
            float xRandom = Random.Range(-planeSize, planeSize);
            float zRandom = Random.Range(-planeSize, planeSize);

            position = new Vector3(xRandom, transform.position.y, zRandom);
            agent.SetDestination(position);

            hasPath = agent.CalculatePath(position, agent.path);

        } while (!hasPath);

        return position;
    }

    protected Vector3 RunAway(Transform attacker)
    {
        this.attacker = attacker;
        bool hasPath;

        position = (transform.position - attacker.position) * 5f;
        agent.SetDestination(position);
        hasPath = agent.CalculatePath(position, agent.path);

        while (!hasPath)
        {
            float xRandom = Random.Range(-planeSize, planeSize);
            float zRandom = Random.Range(-planeSize, planeSize);

            position = new Vector3(xRandom, transform.position.y, zRandom);
            agent.SetDestination(position);

            hasPath = agent.CalculatePath(position, agent.path);
        }
        return position;
    }

    public float TakeDamage(Transform attacker, float amount)
    {
        this.attacker = attacker;
        currentHealth -= amount;
        if (currentHealth > 0)
        {
            float scaleY = healthBar.transform.localScale.y * currentHealth / maxHealth;
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x, scaleY, healthBar.transform.localScale.z);
            ChangeBarColor();
        }

        switch (unitType)
        {
            case UnitType.NPC:
                RunAway(attacker);
                AlertNearbyNPCs();
                break;
            case UnitType.Guard:
                Attack(attacker);
                AlertNearbyNPCs();
                break;
            case UnitType.Player:
                break;
            default: break;
        }

        return currentHealth;
    }

    protected void ChangeBarColor()
    {
        if (currentHealth / maxHealth < 0.33f)
        {
            healthbarRenderer.material.color = Color.red;
        }
        else if (currentHealth / maxHealth < 0.66f)
        {
            healthbarRenderer.material.color = Color.yellow;
        }
        else
        {
            healthbarRenderer.material.color = Color.green;
        }
    }

    public void Attack(Transform target)
    {
        this.target = target;
        hasDestination = true;
        agent.stoppingDistance = 1;
        agent.SetDestination(target.position);
        CheckIfTargetInRange();
        if (inRange && attackCooldown <= 0)
        {
            attackCooldown = attackSpeed;
            float remainingDamage = target.GetComponent<UnitAction>().TakeDamage(transform, damage);
            if (remainingDamage <= 0)
            {
                if (unitType == UnitType.Guard)
                {
                    Debug.Log("YOU DIED!");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {

                    Destroy(target.gameObject);
                    hasDestination = false;
                    inRange = false;
                }
            }
        }
        else
        {
            Move(target.position);
        }
    }

    public void CheckIfTargetInRange()
    {
        inRange = false;
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.remainingDistance <= range && target != null)
        {
            inRange = true;
        }
        else
        {
            Move(target.position);
        }
    }

    private void AlertNearbyNPCs()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, alertRange);
        foreach (var col in colliders)
        {
            if (col.CompareTag("NPC"))
            {
                if (col.transform.GetComponent<NPCMovement>().unitType == UnitType.Guard)
                {
                    col.transform.GetComponent<NPCMovement>().Attack(attacker);
                }
                else
                {
                    col.transform.GetComponent<NPCMovement>().RunAway(attacker);
                }
            }
        }
    }
}
