using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableEntity, IEventHandler
{
    private WeaponBehaviour weapon;

    private void Awake()
    {
        SubscribeEvents();
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life });
        weapon = this.GetComponentInChildren<WeaponBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Mouvement();
        Attack();
    }

    public override void TakeDamage(int damage)
    {
        life -= damage;
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life });
        if (life <= 0)
        {
            //todo event game over
            EventManager.Instance.Raise(new GameOverEvent());
            Destroy(gameObject);
        }
    }

    public override void heal(int n)
    {
        life += n;
        if (life > maxLife) life = maxLife;
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life });
    }

    private void Mouvement()
    {
        float hInput, vInput, mouseX;
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X");

        Move(transform.forward * vInput);
        Move(transform.right * hInput);
        Rotate(Vector3.up * mouseX);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weapon.canAttack()) weapon.Attack();
        }
    }
}