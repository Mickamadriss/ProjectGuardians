using System;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public class Axe : SidedWeapon
{
    [Header("Settings")]
    public float AttackCooldown;
    public bool CanAttack = true;
    private bool IsAttacking = false;
    [Header("References")]
    public AudioClip AttackSound;
    private Animator anim;
    private AudioSource ac;
    private Collider collider;

    private void Awake()
    {
       anim = GetComponent<Animator>();
       ac = GetComponent<AudioSource>();
       collider = GetComponent<Collider>();
    }

    //L'attaque lance une animation, et donc si le collider de la Axe touche un ennemy ça va déclencher le OnTriggerEnter
    public override void Attack()
    {
        if (!CanAttack) return;
        CanAttack = false;
        IsAttacking = true;
        collider.enabled = true;

        //Animation
        anim.SetTrigger("Attack");
        
        //Son
        ac.PlayOneShot(AttackSound);

        //Cooldown de l'arme
        StartCoroutine(ResetAttack());
        StartCoroutine(ResetAttackBool());
    }
    
    //Repasse le CanAttack à true après X secondes
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
    //Repasse le IsAttacking à false après 1 seconde (évite les doubles attaques)
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //On vérifie bien que ce soit un ennemi ce qu'on frappe
        if (other.GetComponentInParent<Entity>() != null)
        {
            if (other.GetComponentInParent<Entity>().getSide() != side && IsAttacking)
            {
                other.GetComponent<AIEnnemy>().TakeDamage(damage);
                IsAttacking = false;
            }
        }
    }
}
