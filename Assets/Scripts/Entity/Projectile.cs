using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float liveTime;
    public int damage;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke(nameof(killProjectile), liveTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        /*if(other.gameObject.GetComponentInParent<IDamageable>() != null && Utils.IsFromOtherSide(gameObject, other.gameObject))
        {
            other.gameObject.GetComponentInParent<IDamageable>().TakeDamage(damage);
        }
        killProjectile();*/
    }

    private void killProjectile()
    {
        Destroy(gameObject);
    }
}
