using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IEventHandler, IAlly
{
    [Header("Parameters")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public GameObject defaultWeapon;
    

    private void Awake()
    {
        SubscribeEvents();
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life });
    }

    private void Start()
    {
        //Spawn du weapon par défaut
        GameObject defaultWeaoponSpawn = Instantiate(defaultWeapon, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
        //Faire que le weapon suive le player (todo: faire aussi la rotation)
        defaultWeaoponSpawn.transform.SetParent(transform, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsPlaying) return;
        
        //Event pour déclencher l'attaque du joueur
        if (Input.GetKeyDown(throwKey))
        {
            //Récupération script weapon
            IWeapon weapon = defaultWeapon.GetComponent<Pistol>();
            
            if (weapon.CanAttack()) weapon.Attack();
        }
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