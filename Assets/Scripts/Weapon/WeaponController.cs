using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private IWeapon weapon;
    private KeyCode throwKey = KeyCode.Mouse0;

    // Update is called once per frame
    void Update()
    {
        //Event pour déclencher l'attaque du joueur
        if (Input.GetKeyDown(throwKey))
        {
            Debug.Log("attack mouse");
            weapon.Attack();
        }
    }
}
