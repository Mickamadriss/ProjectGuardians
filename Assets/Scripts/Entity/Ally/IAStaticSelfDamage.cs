
using System;
using UnityEngine;

public class IAStaticSelfDamage: IAStatic
{
    [SerializeField] private int m_SelfDamage;
    [SerializeField] int m_damageCoolDown;
    private bool canDamage = true;
    protected override void Attack(Vector3 target)
    {
        base.Attack(target);
        
        if (canDamage)
        {
            TakeDamage(m_SelfDamage, gameObject);
            canDamage = false;
            Invoke("ResetCooldown", m_damageCoolDown);
        }
    }

    private void ResetCooldown()
    {
        canDamage = true;
    }
}
