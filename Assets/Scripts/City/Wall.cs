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
        if (other.gameObject.GetComponent<IEnnemy>() != null && time + 5.0f < Time.time)
        {
            EventManager.Instance.Raise(new CityAttacked() { eDamage = 1 });
            time = Time.time;
        }
    }
}
