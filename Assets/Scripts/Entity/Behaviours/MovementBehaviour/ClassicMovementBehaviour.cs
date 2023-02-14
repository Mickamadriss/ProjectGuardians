using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClassicMovementBehaviour : MonoBehaviour, MovementBehaviour
{
    [SerializeField] private GameObject wall;
    private AIEntity m_AIEntity;
    private bool aggro;


    private void Awake()
    {
        //cherche le component AIEntity dans les parents
        m_AIEntity = GetComponentInParent<AIEntity>();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (!aggro)
        {
            m_AIEntity.RotateTo(wall);
        }
        m_AIEntity.Move(transform.forward);
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
