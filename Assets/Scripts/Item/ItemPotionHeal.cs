
using System;
using SDD.Events;
using UnityEngine;
using Event = SDD.Events.Event;

public class ItemPotionHeal : ItemPotion
{
    [Header("Settings")]
    [SerializeField] private int healAmount;

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
