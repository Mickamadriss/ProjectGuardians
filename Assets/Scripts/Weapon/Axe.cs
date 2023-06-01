using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : IWeapon
{
    [Header("Settings")]
    public int Damage;
    public float AttackCooldown;
    public bool CanAttack = true;
    private bool IsAttacking = false;
    [Header("References")]
    public AudioClip AttackSound;

    //L'attaque lance une animation, et donc si le collider de la Axe touche un ennemy ça va déclencher le OnTriggerEnter
    public override void Attack()
    {
        if (!CanAttack) return;
        CanAttack = false;
        IsAttacking = true;

        //Animation
        Animator anim = this.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        
        //Son - NE MARCHE PAS ? OU COMMENT ALLUMER LE SON SUR UNITY
        AudioSource ac = GetComponent<AudioSource>();
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
    }

    private void OnTriggerEnter(Collider other)
    {
        //Si collision avec un ennemi alors dégats
        if (other.GetComponentInParent<AIEnnemy>() != null && IsAttacking)
        {
            other.GetComponent<AIEnnemy>().TakeDamage(Damage);
            IsAttacking = false;
        }
    }
}
