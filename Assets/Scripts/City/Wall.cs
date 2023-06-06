using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public class Wall : MonoBehaviour, IDamageable, ICity
{
    public Side getSide()
    {
        return Side.Ally;
    }
    
    void IDamageable.TakeDamage(int damage, GameObject dammager)
    {
        EventManager.Instance.Raise(new CityAttacked() { eDamage = damage });
    }
}