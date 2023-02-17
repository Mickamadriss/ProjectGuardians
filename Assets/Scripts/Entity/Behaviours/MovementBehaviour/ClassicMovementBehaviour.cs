using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClassicMovementBehaviour : MonoBehaviour, MovementBehaviour
{
    [SerializeField] private GameObject wall;
    private AIEntity m_AIEntity;
    private WeaponBehaviour weapon;
    private bool aggro;
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
        //Debug.Log(aggro);
        if (!aggro)
        {
            m_AIEntity.RotateTo(wall);
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
        if (other.gameObject.GetComponent<IEnnemy>() == null)
        {
            aggro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IEnnemy>() == null)
        {
            aggro = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //test si le game object n'intégres pas l'interface IEnnemy
        if(other.gameObject.GetComponent<IEnnemy>() == null)
        {
            m_AIEntity.RotateTo(other.gameObject);
        }
    }
}
