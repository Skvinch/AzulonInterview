using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private ScrollRect _scrollRect; // ScrollRect that contains all shop item UI elements
    [SerializeField] private ShopItem _shopItemPrefab; // Prefab used to display a single shop item
    [Header("Spawn Settings")]
    [SerializeField] private List<ShopItemData> _shopItemDataList = new(); // List of all items available in the shop
     
    private Inventory _inventory; // Cached reference to the Inventory

    private void OnEnable() => LayoutReset();

    private IEnumerator Start()
    {
        // Cache Inventory reference
        _inventory = Inventory.Instance;

        // Spawn all shop items
        SpawnItems();

        // Wait one frame so LayoutGroup + ContentSizeFitter can calculate sizes
        yield return null;

        // Reset layout and scroll position
        LayoutReset();
    }

    private void LayoutReset()
    {
        // Force full layout rebuild to ensure correct sizing
        LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);

        // Always reset scroll position to the left
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void SpawnItems()
    {
        Transform content = _scrollRect.content;

        // Instantiate UI elements for each shop item
        for (int i = 0; i < _shopItemDataList.Count; i++)
        {
            ShopItem itemUI = Instantiate(_shopItemPrefab, content);

            // Provide Shop reference so ShopItem can call TryBuyItem
            itemUI.Init(_shopItemDataList[i], this);
        }
    }

    // Attempts to purchase an item from the shop
    public void TryBuyItem(ShopItemData item)
    {
        // Check if the item is already owned
        if (_inventory.HasItem(item))
            return;

        // Check if the player has enough coins
        if (!CurrencyManager.Instance.CanAfford(item.ItemPrice))
        {
            Debug.Log("Not enough coins");
            return;
        }

        // Spend coins
        CurrencyManager.Instance.SpendCoins(item.ItemPrice);

        // Add the item to the inventory
        _inventory.AddItem(item);
    }
}
