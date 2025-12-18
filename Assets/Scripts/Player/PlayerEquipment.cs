using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private List<EquipmentSlot> _slots = new(); // List of all equipment slots configured in the Inspector
    
    private Dictionary<int, EquipmentSlot> _slotMap = new(); // Maps SlotID to its corresponding EquipmentSlot for fast lookup
    private Dictionary<int, ShopItemData> _equippedItems = new(); // Stores the currently equipped item per slot

    private void Awake()
    {
        // Build slot lookup table and equip default items
        foreach (var slot in _slots)
        {
            // Register slot by its unique ID
            _slotMap[slot.SlotID] = slot;

            // Equip default item for this slot
            _equippedItems[slot.SlotID] = slot.DefaultItem;
            slot.Renderer.sprite = slot.DefaultItem.ItemIcon;
        }
    }

    private void Start()
    {
        Inventory.Instance.OnItemUsed += Equip;
    }

    private void OnDisable()
    {
        Inventory.Instance.OnItemUsed -= Equip;
    }

    // Called when an item is used from the inventory
    private void Equip(ShopItemData newItem)
    {
        // Determine which slot this item belongs to
        int slotId = newItem.EquipSlotId;

        // Check if a slot with this ID exists
        if (!_slotMap.TryGetValue(slotId, out EquipmentSlot slot))
        {
            Debug.LogWarning($"No slot for SlotId {slotId}");
            return;
        }

        // Swap currently equipped item with the new one
        Swap(slotId, slot, newItem);
    }

    // Handles swapping items between inventory and equipment
    private void Swap(int slotId, EquipmentSlot slot, ShopItemData newItem)
    {
        // Get the item that is currently equipped in this slot
        ShopItemData oldItem = _equippedItems[slotId];

        // Move the new item out of the inventory
        Inventory.Instance.RemoveItem(newItem);

        // Return the previously equipped item back to the inventory
        Inventory.Instance.AddItem(oldItem);

        // Equip the new item
        _equippedItems[slotId] = newItem;
        slot.Renderer.sprite = newItem.ItemIcon;
    }
}

[Serializable]
public class EquipmentSlot
{
    public int SlotID; // Unique identifier used to match items to this slot
    public SpriteRenderer Renderer; // SpriteRenderer where the equipped item's sprite will be displayed
    public ShopItemData DefaultItem; // Default item equipped at game start
}
