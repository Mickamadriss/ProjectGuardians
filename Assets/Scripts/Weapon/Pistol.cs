using STUDENT_NAME.Entity;
using UnityEngine;

public class Pistol : SidedWeapon
{
    [Header("Settings")]
    public float throwCooldown;
    
    [Header("References")]
    public GameObject objectToThrow;
    public Transform attackPoint;
    public Transform cam;
    public bool canAttack = true;

    [Header("Throwing")]
    public float throwForce;

    //Lance un projectile
    public override void Attack()
    {
        if (!canAttack) return;
        canAttack = false;

        //Instancie la balle
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        //--Variables--
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();      //RigidBody du projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>();    //Script du projectile
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