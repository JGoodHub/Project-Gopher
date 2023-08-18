using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BotManager : CharacterManager
{

    public NavMeshAgent _navMeshAgent;
    private Transform _playerLocation;

    public LayerMask PlayerMask;
    public LayerMask GroundMask;

    public float sightRange = 10.0f;
    public float attackRange = 8.0f;
    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool canAttack;

    public float attackCooldown = 0.3f;
    private float attackTimer = 0.0f;


    protected override void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerLocation = GameObject.Find("GopherPlayer").transform;
    }

    protected override void Update()
    {
        CheckRanges();
        SetAttackBool();
        UpdateAttackTimer();
    }

    private void ChasePlayer()
    {
        if (!_playerLocation) return;
        if (playerInSightRange)
        {
            _navMeshAgent.SetDestination(_playerLocation.position);
        }

    }

    private void AttackPlayer()
    {
        _navMeshAgent.SetDestination(transform.position);

        transform.LookAt(_playerLocation);

        if (canAttack)
        {
            _ballAndChainThrower.ThrowChain(_playerLocation.position);
        }

    }

    private void CheckRanges()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerMask);

        if (!playerInAttackRange || !canAttack) GetChains();
        if (playerInSightRange && !playerInAttackRange && canAttack) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && canAttack) AttackPlayer();
    }

    private void SetAttackBool()
    {
        canAttack = attributeSet.heldChains > 0 && attackTimer <= 0.0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    private void GetChains()
    {

        List<GameObject> Pickups = FindChains();
        int RandomPickup = Random.Range(0, Pickups.Count);
        if (Pickups.Count != 0)
        {
            _navMeshAgent.SetDestination(Pickups[RandomPickup].transform.position);
        }
    }

    private List<GameObject> FindChains()
    {
        GameObject[] AllObjects;
        List<GameObject> Pickups = new List<GameObject>();

        AllObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in AllObjects)
        {
            if (obj.name == "BallAndChainPickup(Clone)" || obj.name == "BallAndChainPickup")
            {
                Pickups.Add(obj);
            }
        }

        return Pickups;
    }

    private void UpdateAttackTimer()
    {
        if (canAttack)
        {
            attackTimer = attackCooldown;
        }

        attackTimer -= Time.deltaTime;
    }
}
