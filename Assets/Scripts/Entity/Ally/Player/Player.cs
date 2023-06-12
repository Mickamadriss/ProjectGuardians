using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;
using UnityEngine.Analytics;

public class Player : Entity, IEventHandler, IAlly
{

    [SerializeField] private float exp;
    [SerializeField] private float expNeeded;
    [SerializeField] private int gold;

    private void Awake()
    {
        SubscribeEvents();
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life / maxLife * 100 });
    }

    private void Start() { }

    // Update is called once per frame
    void Update() { }

    public override Side getSide()
    {
        return Side.Ally;
    }

    public override void TakeDamage(int damage, GameObject dammager)
    {
        life -= damage;
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = (life / maxLife) * 100 });
        if (life <= 0)
        {
            kill(dammager);
        }
    }

    public override void heal(int n)
    {
        life += n;
        if (life > maxLife) life = maxLife;
        EventManager.Instance.Raise(new PlayerLifeChanged() { eLife = life / maxLife * 100 });
    }

    private void levelUp()
    {
        maxLife = maxLife + 10;
        heal((int) maxLife);
        expNeeded += 10;
        exp = 0;
    }

    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<EnnemyKilled>(ennemyKilled);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(mainMenuHasBeenCliked);
        EventManager.Instance.AddListener<PlayerGoldUpdate>(playerGoldUpdate);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<EnnemyKilled>(ennemyKilled);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(mainMenuHasBeenCliked);
    }

    private void ennemyKilled(EnnemyKilled e)
    {
        if (e.ePlayerKill)
        {
            exp += e.eEntity.exp;
        }
        else
        {
            exp += (float)(e.eEntity.exp * 0.5);
        }
        setGold(gold + e.eEntity.gold);
        if (exp >= expNeeded)
        {
            levelUp();
        }
        EventManager.Instance.Raise(new PlayerExpChanged() { eExp = exp / expNeeded * 100 });
    }

    private void setGold(int gold)
    {
        this.gold = gold;
        EventManager.Instance.Raise(new PlayerGoldChanged() { eGold = gold});
    }

    private void mainMenuHasBeenCliked(MainMenuButtonClickedEvent e)
    {
        Destroy(gameObject);
    }

    public override void kill(GameObject killer)
    {
        //todo event game over
        EventManager.Instance.Raise(new GameOverEvent());
        Destroy(gameObject);
    }

    private void playerGoldUpdate(PlayerGoldUpdate e)
    {
        setGold(gold + e.Gold);
    }
    
    public void BuyItem(IWeapon weaponToBuy, int price)
    {
        //Si on à l'argent : on achète / équipe l'item, et perd l'argent
        if (gold >= price)
        {
            // Code pour équiper l'item //
            setGold(gold - price);
        }

        //Si on n'a pas l'argent : popup "t'es pauvre"
        else
        {
            // Code pour afficher une popup //
        }
    }
}