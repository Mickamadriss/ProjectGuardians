using STUDENT_NAME.Entity;
using UnityEngine;

public class Pistol : SidedWeapon
{
    [Header("Settings")]
    public float throwCooldown;
    
    [Header("References")]
    [SerializeField] Projectile projectileObject = null;
    public Transform attackPoint;
    public bool canAttack = true;

    [Header("Throwing")]
    public float throwForce;

    //Lance un projectile
    public override void Attack()
    {
        if (!canAttack) return;
        canAttack = false;

        //Instancie la balle
        GameObject projectile = Instantiate(projectileObject.gameObject, attackPoint.position, attackPoint.rotation);
        //--Variables--
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();      //RigidBody du projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>();    //Script du projectile
        projectileScript.damage = damage;
        projectileScript.side = side;

        //Calcul direction
        Vector3 forceDirection = attackPoint.transform.forward;

        //Ajout force sur la balle
        Vector3 forceToAdd =  forceDirection * throwForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        //Repasse le canAttack à true dans X secondes
        Invoke(nameof(resetThrow), throwCooldown);
    }
    
    //Repasse le canAttack à true
    private void resetThrow()
    {
        canAttack = true;
    }
}