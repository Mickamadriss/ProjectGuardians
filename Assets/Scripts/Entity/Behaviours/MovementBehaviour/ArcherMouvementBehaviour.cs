using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ArcherMouvementBehaviour : MouvementBehaviour
{
    private bool RunningAway = false;

    public override bool isTarget(GameObject g)
    {
        throw new System.NotImplementedException();
    }

    void FixedUpdate()
    {
        //si on aggro personne se diriger vers la cible et avancer
        if (aggro == null)
        {
            m_AIEntity.RotateTo(m_AIEntity.destination);
            m_AIEntity.Move(transform.forward);
        }else
        { 
            if (weapon.canAttack())
            {
                weapon.Attack();
            } else
            {
                m_AIEntity.Move(transform.forward); //si on peut pas attaquer il faut soit fuir soit se rapprocher de l'ennemi
            }
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
        //todo si on peut attaquer alors se tourner vers la cible
        //sinon on se tourne à l'opposer
        //ne pas oublié le cas d'un ally déjà présent quand l'aggro == null
    }
}
