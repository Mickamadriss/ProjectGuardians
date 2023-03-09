using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float coolDown = 2.0f;
    protected bool isAttacking = false;
    protected float timerCoolDown;

    abstract public void Attack();
    abstract public bool canAttack();

    abstract public bool getIsAttacking();
}
