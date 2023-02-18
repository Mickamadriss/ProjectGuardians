using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClassicMovementBehaviour : MonoBehaviour
{
    private AIEntity m_AIEntity;
    private WeaponBehaviour weapon;
    private GameObject aggro = null;
    private float timerAttack = 0.0f;


    private void Awake()
    {
        //cherche le component AIEntity dans les parents
        m_AIEntity = GetComponentInParent<AIEntity>();
        weapon = m_AIEntity.GetComponentInChildren<WeaponBehaviour>();
    }

    //todo passer en fixed update
    void Update()
    {
        if (aggro == null)
        {
            m_AIEntity.RotateTo(m_AIEntity.destination);
        }
        m_AIEntity.Move(transform.forward);

        if (aggro)
        {
            if (timerAttack > 5.0f)
            {
                weapon.Attack();
                timerAttack = 0.0f;
            }
            timerAttack += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (aggro == null && other.gameObject.GetComponent<IEnnemy>() == null)
        {
            aggro = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IEnnemy>() == null && other.gameObject == aggro.gameObject)
        {
            aggro = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<IEnnemy>() == null && other.gameObject == aggro.gameObject)
        {
            m_AIEntity.RotateTo(other.gameObject.transform);
        }
    }
}
