using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAStatic : Entity
{
    // turret head
    public Transform head;
    public IWeapon weapon;
    
    public Transform target;
    public LayerMask whatIsEnnemy;
    
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform attackPoint;
    
    //States
    public float attackRange;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Collider[] targetInAttackRange = Physics.OverlapSphere(transform.position, attackRange, whatIsEnnemy);
        
        if (targetInAttackRange.GetLength(0) == 0) return;

        target = targetInAttackRange[0].gameObject.transform;
        Attack(target.position);
    }
    
    private void Attack(Vector3 target)
    {
        head.LookAt(target);

        weapon.Attack();
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
