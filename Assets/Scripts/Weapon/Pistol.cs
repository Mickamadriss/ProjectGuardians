using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public class Pistol : IWeapon
{
    [Header("Settings")]
    public float AttackCooldown;
    public bool CanAttack = true;

    [Header("References")]
    public GameObject objectToThrow;
    public Transform attackPoint;
    public Transform cam;

    [Header("Throwing")]
    public float throwForce;

    //Lance un projectile
    public override void Attack()
    {
        if (!CanAttack) return;
        CanAttack = false;

        //Instancie la balle
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        //--Variables--
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>(); //RigidBody du projectile
        Projectile projectileScript = projectile.GetComponent<Projectile>(); //Script du projectile
        projectileScript.side = Side.Ally;

        //Calcul direction
        Vector3 forceDirection = attackPoint.transform.forward;

        //Ajout force sur la balle
        Vector3 forceToAdd = forceDirection * throwForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        //Cooldown de l'arme
        StartCoroutine(ResetAttack());
    }

    //Repasse le CanAttack à true après X secondes
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}