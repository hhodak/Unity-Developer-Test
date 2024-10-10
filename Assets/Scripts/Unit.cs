using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum UnitType
{
    Player,
    NPC,
    Guard
}

public class Unit : MonoBehaviour
{
    public UnitType unitType;

    protected NavMeshAgent agent;
    protected bool hasDestination = false;

    [SerializeField] protected GameObject healthBar;
    [SerializeField] protected GameObject AlertRangeSprite;
    protected Renderer healthbarRenderer;
    protected Renderer renderer;
    protected float currentHealth;
    protected float maxHealth = 10;

    protected float damage = 2;
    protected float range = 1f;
    protected bool inRange = false;
    protected Transform target;
    protected Transform attacker;
    protected float alertRange = 5f;
    protected float attackSpeed = 2f;
    [SerializeField] protected float attackCooldown;

    protected Vector3 position;
    protected float planeSize = 25f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        renderer = GetComponent<Renderer>();
        switch (unitType)
        {
            case UnitType.NPC:
                renderer.material.color = Color.cyan;
                break;
            case UnitType.Guard:
                renderer.material.color = Color.blue;
                AlertRangeSprite.SetActive(true);
                break;
            default: break;
        }
        healthbarRenderer = healthBar.GetComponent<Renderer>();
        healthbarRenderer.material.color = Color.green;
        attackCooldown = attackSpeed;
    }
}
