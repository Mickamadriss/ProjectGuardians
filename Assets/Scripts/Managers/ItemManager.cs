using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [Header("Items")]
    [SerializeField] private Item meleeWeaponItem;
    [SerializeField] private Item rangeWeaponItem;
    private Item[] _items;

    [Header("KeyBinds")]
    public KeyCode meleeWeaponItemKey = KeyCode.Alpha1;
    public KeyCode rangeWeaponItemKey = KeyCode.Alpha2;
    private KeyCode[] _keyCodes;
    
    private void Awake()
    {
        // Order is important
        _items = new[] { meleeWeaponItem, rangeWeaponItem };
        _keyCodes = new[] { meleeWeaponItemKey, rangeWeaponItemKey };
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
    }

    private void enableItem(int itemIndex)
    {
        disableAllItems();
        _items[itemIndex].enabled = true;
    }

    private void disableAllItems()
    {
        foreach (var item in _items)
        {
            item.enabled = false;
        }
    }
}
