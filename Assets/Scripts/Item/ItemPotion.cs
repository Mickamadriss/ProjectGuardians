using System;
using UnityEngine;
public abstract class ItemPotion : Item
{
    [SerializeField] private GameObject potion;
    [SerializeField] private int maxPotionCount;
    [SerializeField] private int potionCount;
    
    [Header("KeyBinds")]
    public KeyCode useKey = KeyCode.Mouse0;

    private void OnEnable()
    {
        potion.SetActive(true);
    }

    private void OnDisable()
    {
        potion.SetActive(false);
    }
    
    protected virtual bool UsePotion()
    {
        if (potionCount <= 0) return false;
        potionCount--;
        return true;
    }

    public void Refill(int quantity)
    {
        potionCount += quantity;
        if (potionCount > maxPotionCount) potionCount = maxPotionCount;
    }
}
        
