using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float liveTime;
    public int damage;
    public Side side;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(killProjectile), liveTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        //On ne peut attaquer qu'un gameObject damageable
        IDamageable target = other.gameObject.GetComponentInParent<IDamageable>();
        if (target != null)
        {
            //Détecter que c'est bien une collision avec le camp ennemi
            if (this.side != target.getSide())
            {
                //Applique l'attaque + détruit le projectile
                target.TakeDamage(damage);
                killProjectile();
            }
            
        }
    }

    private void killProjectile()
    {
        Destroy(gameObject);
    }
}
