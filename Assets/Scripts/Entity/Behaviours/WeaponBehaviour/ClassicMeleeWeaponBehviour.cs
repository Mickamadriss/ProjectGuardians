using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMeleeWeaponBehviour : WeaponBehaviour
{
    
    [SerializeField] private float durationAttack = 1.0f;
    private float timerDurationAttack;
    private bool isAttacking = false;
    private Collider m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
    }

    private void Update()
    {
        //si la période d'attaque est fini
        if (isAttacking && Time.time - timerDurationAttack > durationAttack)
        {
            isAttacking = false;
            m_Collider.enabled = false;
        }
    }

    public override void Attack()
    {
        timerDurationAttack = Time.time;
        timerCoolDown = Time.time;
        isAttacking = true;
        m_Collider.enabled = true;
    }

    public override bool canAttack()
    {
        return !isAttacking && Time.time - timerCoolDown > coolDown;
    }

    public override bool getIsAttacking()
    {
        return isAttacking;
    }

    private bool IsFromOtherSide(Collider other)
    {
        //Si this = player && other == enemy
        if (this.gameObject.GetComponent<IEnnemy>() == null)
            return other.gameObject.GetComponent<IEnnemy>() != null;
        
        //Si this = enemy && other == player
        if (this.gameObject.GetComponent<IEnnemy>() != null)
            return other.gameObject.GetComponent<IEnnemy>() == null;

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //todo /!\ si le collider est du même camps que l'ennemi, ne pas le toucher
        if (other.gameObject.GetComponent<IDamageable>() != null && getIsAttacking() && this.IsFromOtherSide(other) /*other.gameObject.GetComponent<IEnnemy>() == null*/)
        {
            if (this.gameObject.GetComponent<IEnnemy>() == null)
                Debug.Log("Player attaque en l'état: "+ getIsAttacking());
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
