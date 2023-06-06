using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIWeapon : Item
{
    [SerializeField] private GameObject weapon;
    
    [Header("KeyBinds")]
    public KeyCode AttackKey = KeyCode.Mouse0;

    private void OnEnable()
    {
        weapon.SetActive(true);
    }

    private void OnDisable()
    {
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(AttackKey))
        { 
           weapon.GetComponentInChildren<IWeapon>().Attack();
        }
    }
}
