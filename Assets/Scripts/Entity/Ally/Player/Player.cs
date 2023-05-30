using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public class Player : Entity, IEventHandler, IAlly
{
    [Header("Parameters")]
    [SerializeField] private IWeapon weapon;
    public KeyCode throwKey = KeyCode.Mouse0;

    private void Awake()
    {
        SubscribeEvents();
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life });
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsPlaying) return;
        
        //Event pour déclencher l'attaque du joueur
        if (Input.GetKeyDown(throwKey))
        {
            weapon.Attack();
        }
    }

    public override Side getSide()
    {
        return Side.Ally;
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
}