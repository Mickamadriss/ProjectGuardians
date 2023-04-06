using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMeleeWeaponBehviour : WeaponBehaviour
{
    
    [SerializeField] private float durationAttack = 1.0f;
    [SerializeField] private GameObject weaponGFX;
    private float timerDurationAttack;
    private bool isAttacking = false;
    private Collider m_Collider;
    

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
        weaponGFX.GetComponent<Renderer>().material.color = Color.white;
    }

    private void Update()
    {
        //si la période d'attaque est fini
        if (isAttacking && Time.time - timerDurationAttack > durationAttack)
        {
            isAttacking = false;
            m_Collider.enabled = false;
            weaponGFX.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public override void Attack()
    {
        timerDurationAttack = Time.time;
        timerCoolDown = Time.time;
        isAttacking = true;
        m_Collider.enabled = true;
        weaponGFX.GetComponent<Renderer>().material.color = Color.red;
    }

    public override bool canAttack()
    {
        return !isAttacking && Time.time - timerCoolDown > coolDown;
    }

    public override bool getIsAttacking()
    {
        return isAttacking;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null && getIsAttacking() && Utils.IsFromOtherSide(gameObject, other.gameObject))
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
