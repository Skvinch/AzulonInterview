using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // Singleton instance for global access
    
    public event Action<ShopItemData> OnItemUsed; // Event fired when an item is used (e.g. equipped by the player)
    public event Action OnInventoryChanged; // Event fired whenever the inventory content changes
    
    private List<ShopItemData> _items = new List<ShopItemData>(); // Internal list that stores all owned items

    private void Awake()
    {
        // Ensure only one Inventory instance exists
        if (Instance == null) 
            Instance = this;
        else Destroy(gameObject);
    }

    // Checks if the inventory already contains the given item
    public bool HasItem(ShopItemData data)
    {
        return _items.Contains(data);
    }

    // Adds an item to the inventory
    public bool AddItem(ShopItemData data)
    {
        // Prevent duplicate items
        if (_items.Contains(data))
            return false;

        _items.Add(data);

        // Notify listeners that inventory has changed
        OnInventoryChanged?.Invoke();
        return true;
    }

    // Returns a read-only list of inventory items
    public IReadOnlyList<ShopItemData> GetItems()
    {
        return _items;
    }

    // Uses an item from the inventory (does not remove it automatically)
    public void UseItem(ShopItemData data)
    {
        // Ignore if the item is not in the inventory
        if (!_items.Contains(data)) return;

        // Notify listeners that this item was used
        OnItemUsed?.Invoke(data);
    }
    
    // Removes an item from the inventory
    public void RemoveItem(ShopItemData data)
    {
        if (_items.Remove(data))
        {
            // Notify listeners that inventory has changed
            OnInventoryChanged?.Invoke();
        }
    }
}