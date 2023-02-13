using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private float time;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnnemy>() != null)
        {
            time = Time.time;
            EventManager.Instance.Raise(new CityAttacked() { eDamage = 1 });
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //test si l'objet intégre l'interface IAttackable et si ça fait 1 seconde
        Debug.Log(other.gameObject.GetComponent<IEnnemy>());
        if (other.gameObject.GetComponent<IEnnemy>() != null && time + 5.0f < Time.time)
        {
            Debug.Log("Wall hit by " + other.gameObject.name);
            EventManager.Instance.Raise(new CityAttacked() { eDamage = 1 });
            time = Time.time;
        }
    }
}
