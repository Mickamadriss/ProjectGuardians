using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME;
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
    void Update() {}

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
        EventManager.Instance.AddListener<PlayerGoldChanged>(playerGoldChanged);
        EventManager.Instance.AddListener<PlayerHealUpdate>(playerHealUpdate);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<EnnemyKilled>(ennemyKilled);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(mainMenuHasBeenCliked);
        EventManager.Instance.RemoveListener<PlayerGoldUpdate>(playerGoldUpdate);
        EventManager.Instance.RemoveListener<PlayerGoldChanged>(playerGoldChanged);
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
        setGold(0);
        EventManager.Instance.Raise(new PlayerExpChanged() { eExp = 0 / expNeeded * 100 });
        Destroy(gameObject);
    }

    private void playerGoldChanged(PlayerGoldChanged e)
    {
        gold = (int) e.eGold;
    }
    private void playerGoldUpdate(PlayerGoldUpdate e)
    {
        setGold(gold + e.Gold);
    }

    private void playerHealUpdate(PlayerHealUpdate e)
    {
        heal(e.Health);
    }

    public void BuyItem(GameObject newWeapon)
    {
        //Récupération gold (ne marche pas avec this.gold)
        HudManager hud = GameObject.Find("HudManager").GetComponent<HudManager>();
        int playerGold = int.Parse(hud.m_NumberGold.text);
        //Récupération script newWeapon, pour des infos
        SidedWeapon newWeaponScript;
        int weaponIndex;
        switch (newWeapon.name)
        {
            case "Pistol":
                weaponIndex = 1;
                newWeaponScript = newWeapon.GetComponent<Pistol>();
                break;
            case "Mace":
                weaponIndex = 0;
                newWeaponScript = newWeapon.GetComponent<Mace>();
                break;
            default:
                weaponIndex = 0;
                newWeaponScript = newWeapon.GetComponent<Mace>();
                break;
        }
        int weaponPrice = newWeaponScript.price;

        
        //Vérification assez d'argent
        if (playerGold >= weaponPrice)
        {
            //Ajout du prefab de la new arme au WeaponHolder
            GameObject wpHolder = GameObject.Find("WeaponHolder");
            GameObject newWeaponPrefab = Instantiate(newWeapon, wpHolder.transform);
            Debug.Log(newWeapon);
            //Récupération du Player clone
            GameObject player = GameObject.Find("Player(Clone)");
            
            switch (newWeapon.name)
            {
                case "Pistol":
                    //S'il existe déjà alors on supp
                    if (GameObject.Find("RangeWeapon"))
                    {
                        Destroy(GameObject.Find("RangeWeapon"));
                    }
                    newWeaponPrefab.name = "RangeWeapon";
                    //Set position
                    newWeaponPrefab.transform.localPosition = new Vector3(.75f, -0.3f, .63f);
                    break;
                case "Mace":
                    //S'il existe déjà alors on supp
                    if (GameObject.Find("MeleeWeapon"))
                    {
                        Destroy(GameObject.Find("MeleeWeapon"));
                    }
                    newWeaponPrefab.name = "MeleeWeapon";
                    newWeaponPrefab.transform.localPosition = new Vector3(.56f, -.71f, .81f);
                    break;
            }

        
            //Edit de l'item
            ItemIWeapon rangeWeapon = player.GetComponents<ItemIWeapon>()[weaponIndex];
            rangeWeapon.weapon = newWeaponPrefab;
            rangeWeapon.enabled = false;
            newWeaponPrefab.SetActive(false);
            
            //On perd l'argent
            EventManager.Instance.Raise(new PlayerGoldUpdate(-weaponPrice));
        }
    }
}