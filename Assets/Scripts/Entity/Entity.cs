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
        if(life <= 0)
        {
            Destroy(this); // ATTENTION PAS DUTOUT OPTIMAL, déjà psk le player ne peut pas disparaître comme ça
        }
    }

    private void heal(int n)
    {
        life += n;
    }
}
