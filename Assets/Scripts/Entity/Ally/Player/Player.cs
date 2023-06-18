using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;
using UnityEngine.Analytics;
using Event = SDD.Events.Event;

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
        EventManager.Instance.AddListener<PlayerHealUpdate>(playerHealUpdate);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<EnnemyKilled>(ennemyKilled);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(mainMenuHasBeenCliked);
        EventManager.Instance.RemoveListener<PlayerGoldUpdate>(playerGoldUpdate);
        EventManager.Instance.RemoveListener<PlayerHealUpdate>(playerHealUpdate);
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
        EventManager.Instance.Raise(new GameOverEvent());
        Destroy(gameObject);
    }

    private void playerGoldUpdate(PlayerGoldUpdate e)
    {
        setGold(gold + e.Gold);
        Debug.Log(this.gold);
    }

    private void playerHealUpdate(PlayerHealUpdate e)
    {
        heal(e.Health);
    }
    
    public void BuyItem(GameObject newWeapon)
    {
        Debug.Log("achat item avec: "+ this.gold);
        //Ajout du prefab requis au WeaponHolder
        GameObject wpHolder = GameObject.Find("WeaponHolder");
        GameObject newWeaponPrefab = Instantiate(newWeapon, wpHolder.transform);

        int weaponIndex = 0;
        switch (newWeapon.name)
        {
            case "Pistol":
                weaponIndex = 1;
                newWeaponPrefab.name = "Pistol";
                newWeaponPrefab.transform.position = wpHolder.transform.position + new Vector3(0f, -0.3f, 0.75f);
                break;
        }

        //Récupération du Player clone
        GameObject player = GameObject.Find("Player(Clone)");
        //Edit de l'item
        ItemIWeapon rangeWeapon = player.GetComponents<ItemIWeapon>()[weaponIndex];
        rangeWeapon.weapon = newWeaponPrefab;
        rangeWeapon.enabled = false;
        
        
        
        //Ajout de l'item au ItemManager
        // GetComponent<ItemManager>().RangeWeaponItem = newItem;
        
        // //MaJ du gold du joueur
        // setGold(gold - price);
        // }

        //Si on n'a pas l'argent : popup "t'es pauvre"
        // else
        // {
        //     Debug.Log("achat impossible, gold :"+gold);
        // }
    }
}