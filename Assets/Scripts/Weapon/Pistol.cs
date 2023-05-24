using UnityEngine;

public class Pistol : IWeapon
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
        Debug.Log("Attacking with pistol...");
        canAttack = false;

        //instantiate object to throw
        //todo: faire en sorte que attackPoint et cam proviennent de la cam du player et non de la copie dégueulasse dans le prefab de Pistol
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        //get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //calculate direction
        Vector3 forceDirection = attackPoint.transform.forward;

        //add force
        Vector3 forceToAdd =  forceDirection * throwForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        Invoke(nameof(resetThrow), throwCooldown);
    }
    
    private void resetThrow()
    {
        canAttack = true;
    }
}