using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicWeaponBehviour : MonoBehaviour, WeaponBehaviour
{
    [SerializeField] private int damage;
    private float timerAttack;
    private float durationAttack = 1.0f;
    private bool isAttacking = false;
    private Collider m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled = false;
    }

    private void Update()
    {
        if (isAttacking && Time.time - timerAttack > durationAttack)
        {
            isAttacking = false;
            m_Collider.enabled = false;
        }
    }

    public void Attack()
    {
        timerAttack = Time.time;
        isAttacking = true;
        m_Collider.enabled = true;
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
