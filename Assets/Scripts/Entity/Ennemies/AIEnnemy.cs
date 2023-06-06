using DG.Tweening;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;
using UnityEngine.AI;

public class AIEnnemy : Entity, IEventHandler
{
    public NavMeshAgent agent;

    public Transform player;
    public Transform city;
    public float exp;

    public LayerMask whatIsGround, whatIsPlayer, whatIsCity;

    //Patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    //Attacking
    [SerializeField] private IWeapon weapon;

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

    public override Side getSide()
    {
        return Side.Ennemy;
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

        weapon.Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public override void kill(GameObject killer)
    {
        bool isPlayer = killer.layer == whatIsPlayer;
        EventManager.Instance.Raise(new EnnemyKilled() { eEntity = this , ePlayerKill = isPlayer});
        Destroy(gameObject);
    }
}
