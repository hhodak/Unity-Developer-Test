using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected bool hasDestination = false;

    [SerializeField] protected GameObject healthBar;
    protected Renderer healthbarRenderer;
    protected Renderer renderer;
    protected float currentHealth;
    protected float maxHealth = 10;

    protected float damage = 2;
    protected float range = 1f;
    protected bool inRange = false;
    protected Transform target;
    protected Transform attacker;

    protected Vector3 position;
    protected float planeSize = 25f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        renderer = GetComponent<Renderer>();
        switch (transform.tag)
        {
            case "NPC":
                renderer.material.color = Color.cyan;
                break;
            case "Guard":
                renderer.material.color = Color.blue;
                break;
            default: break;
        }
        healthbarRenderer = healthBar.GetComponent<Renderer>();
        healthbarRenderer.material.color = Color.green;
    }
}
