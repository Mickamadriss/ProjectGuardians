using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouvementBehaviour : MonoBehaviour
{
    protected AIEntity m_AIEntity;
    protected WeaponBehaviour weapon;
    protected GameObject aggro = null;

    private void Awake()
    {
        //cherche le component AIEntity dans les parents
        m_AIEntity = GetComponentInParent<AIEntity>();
        weapon = m_AIEntity.GetComponentInChildren<WeaponBehaviour>();
    }

    void FixedUpdate()
    {
        if (aggro == null)
        {
            m_AIEntity.RotateTo(m_AIEntity.destination);
        }
        else
        {
            m_AIEntity.RotateTo(aggro.transform);
            if (weapon.canAttack())
            {
                weapon.Attack();
            }
        }
        m_AIEntity.Move(transform.forward);
    }

    public abstract bool isTarget(GameObject g);

    private void OnTriggerExit(Collider other)
    {
        //si le gameobject d'agro sort alors on est plus en aggro
        if (other.gameObject == aggro.gameObject)
        {
            aggro = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //si on se rend compte qu'une autre cible est dans notre zone alors l'aggro
        if (isTarget(other.gameObject)) 
        {
            aggro = other.gameObject;
        }
    }
}
