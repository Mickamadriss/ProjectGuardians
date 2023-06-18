using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAStatic : Entity
{
    public IWeapon weapon;
    [SerializeField] Slider m_HealthBar;
    
    public Transform target;
    public LayerMask whatIsEnnemy;

    //States
    public float attackRange;


    private void Start()
    {
        m_HealthBar.value = life * 100 / maxLife;
    }

    public override void TakeDamage(int damage, GameObject dammager)
    {
        base.TakeDamage(damage, dammager);
        m_HealthBar.value = life * 100 / maxLife;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Collider[] targetInAttackRange = Physics.OverlapSphere(transform.position, attackRange, whatIsEnnemy);
        
        if (targetInAttackRange.GetLength(0) == 0) return;

        target = targetInAttackRange[0].gameObject.transform;
        Attack(target.position);
    }
    
    protected virtual void Attack(Vector3 target)
    {
        weapon.Attack();
    }

    public override void kill(GameObject killer)
    {
        Destroy(gameObject);
    }
}