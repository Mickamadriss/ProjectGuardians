using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IDamageable
{
    void IDamageable.TakeDamage(int damage)
    {
        EventManager.Instance.Raise(new CityAttacked() { eDamage = damage });
    }
}