using System;
using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item MeleeWeaponItem
    {
        get => meleeWeaponItem;
        set => meleeWeaponItem = value;
    }

    public Item RangeWeaponItem
    {
        get => rangeWeaponItem;
        set => meleeWeaponItem = value;
    }

    public Item TurretItem
    {
        get => turretItem;
        set => turretItem = value;
    }

    public ItemPotion PotionItem
    {
        get => potionItem;
        set => potionItem = value;
    }

    [Header("Items")]
    [SerializeField] private Item meleeWeaponItem = null;
    [SerializeField] private Item rangeWeaponItem = null;
    [SerializeField] private Item turretItem = null;
    [SerializeField] private Item trapItem;
    [SerializeField] private ItemPotion potionItem = null;
    private Item[] _items;

    [Header("KeyBinds")]
    public KeyCode meleeWeaponItemKey = KeyCode.Alpha1;
    public KeyCode rangeWeaponItemKey = KeyCode.Alpha2;
    public KeyCode turretItemKey = KeyCode.Alpha3;
    public KeyCode trapItemKey = KeyCode.Alpha4;
    public KeyCode potionItemKey = KeyCode.Alpha5;
    private KeyCode[] _keyCodes;
    
    private void Awake()
    {
        // Order is important
        _items = new[] { meleeWeaponItem, rangeWeaponItem, turretItem, trapItem, potionItem };
        _keyCodes = new[] { meleeWeaponItemKey, rangeWeaponItemKey, turretItemKey, trapItemKey, potionItemKey };
    }

    // Start is called before the first frame update
    void Start()
    {
        disableAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        for (int itemIndex = 0; itemIndex < _keyCodes.GetLength(0); itemIndex++)
        {
            if (Input.GetKeyDown(_keyCodes[itemIndex]))
            {
                enableItem(itemIndex);
            }
        }
        //
        // Debug.Log(_items.Length);
        // Debug.Log(_items.GetValue(0));
        // Debug.Log(_items.GetValue(1));
    }

    private void enableItem(int itemIndex)
    {
        disableAllItems();
        _items[itemIndex].enabled = true;
        EventManager.Instance.Raise(new SelectedItemChangedEvent(itemIndex));
    }

    private void disableAllItems()
    {
        foreach (var item in _items)
        {
            if (item != null)
                item.enabled = false;
        }
    }

    public void replaceItem(int index, Item item)
    {
        // Debug.Log(_items.Length);
        // _items[index] = item;
        // Debug.Log(item);
        // RangeWeaponItem = item;
    }

    public void RefillPotion(int quantity)
    {
        potionItem.Refill(quantity);
    }
    
    
}
