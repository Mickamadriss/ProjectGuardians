using DG.Tweening;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnnemy : Entity, IEventHandler
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform city;

    public LayerMask whatIsGround, whatIsPlayer, whatIsCity;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Collider[] cityWallInSight = Physics.OverlapSphere(transform.position, sightRange, whatIsCity);
        Collider[] cityWallInAttack = Physics.OverlapSphere(transform.position, attackRange, whatIsCity);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && cityWallInSight.Length == 0) Patroling();
        if (playerInSightRange && !playerInAttackRange) Chase(player.position);
        if (cityWallInSight.Length != 0) Chase(cityWallInSight[0].gameObject.transform.position);
        if (playerInSightRange && playerInAttackRange) Attack(player.position);
        if (cityWallInAttack.Length != 0) Attack(cityWallInAttack[0].gameObject.transform.position);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Raise(new EnnemyKilled() { eEntity = this});
    }

    private void Patroling()
    {
        agent.SetDestination(city.position);
    }

    private void Chase(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Attack(Vector3 target)
    {
        agent.SetDestination(transform.position);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            //ATTACK
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 7f, ForceMode.Impulse);
            rb.AddForce(transform.up * 0f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
