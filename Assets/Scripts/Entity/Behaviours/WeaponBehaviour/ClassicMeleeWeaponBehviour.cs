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

    private void OnTriggerEnter(Collider other)
    {
        //todo /!\ si le collider est du même camps que l'ennemi, ne pas le toucher
        if (other.gameObject.GetComponent<IDamageable>() != null && isAttacking && other.gameObject.GetComponent<IEnnemy>() == null)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
