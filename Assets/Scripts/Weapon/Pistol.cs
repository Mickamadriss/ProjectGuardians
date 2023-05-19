using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [Header("Settings")]
    public float throwCooldown;
    
    [Header("References")]
    public GameObject objectToThrow;
    public Transform attackPoint;
    public Transform cam;
    
    [Header("Throwing")]
    public float throwForce;
    private bool readyToThrow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    //Indique si oui ou non l'arme est prête à être utilisée
    public bool CanAttack()
    {
        //readyToThrow = true après X secondes de cooldown
        return readyToThrow;
    }
    
    //Lance un projectile
    public void Attack()
    {
        Debug.Log("Attacking with pistol...");
        readyToThrow = false;

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
        readyToThrow = true;
    }
}