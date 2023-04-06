using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected int life;
    [SerializeField] protected int maxLife;

    public virtual void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log(life);
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void heal(int n)
    {
        life += n;
        if (life > maxLife) life = maxLife;
    }
}
