using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private int life;

    public void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log(life);
        if(life <= 0)
        {
            Destroy(this); // ATTENTION PAS DUTOUT OPTIMAL, d?j? psk le player ne peut pas dispara?tre comme ?a
        }
    }

    private void heal(int n)
    {
        life += n;
    }
}
