
using System;
using SDD.Events;
using UnityEngine;
using Event = SDD.Events.Event;

public class ItemPotionHeal : ItemPotion, IEventHandler
{
    [Header("Settings")]
    [SerializeField] private int healAmount;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayerPotionRestart>(restartPotion);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayerPotionRestart>(restartPotion);
    }

    private void restartPotion(PlayerPotionRestart e )
    {
        potionCount = maxPotionCount;
    }

    protected override bool UsePotion()
    {
        if(!base.UsePotion()) return false ;
        EventManager.Instance.Raise(new PlayerHealUpdate(healAmount));
        return true;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(useKey)) return;
        UsePotion();
    }
}
