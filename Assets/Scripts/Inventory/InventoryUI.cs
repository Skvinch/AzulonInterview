using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Prefab used to represent a single inventory item in the UI
    [SerializeField] private InventoryItem _inventoryItemPrefab;

    // ScrollRect that contains the inventory content
    [SerializeField] private ScrollRect _scrollRect;
    
    private void OnEnable()
    {
        // Subscribe to inventory changes to refresh UI automatically
        Inventory.Instance.OnInventoryChanged += Refresh;

        // Initial UI build
        Refresh();

        // Reset layout and scroll position
        LayoutReset();
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        Inventory.Instance.OnInventoryChanged -= Refresh;
    }
    
    private void LayoutReset()
    {
        // Force layout recalculation (LayoutGroup + ContentSizeFitter)
        LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);

        // Always reset scroll position to the left
        _scrollRect.horizontalNormalizedPosition = 0f;
    }

    private void Refresh()
    {
        // Clear existing inventory UI items
        foreach (Transform child in _scrollRect.content)
            Destroy(child.gameObject);

        // Recreate UI items based on current inventory data
        foreach (var item in Inventory.Instance.GetItems())
        {
            InventoryItem invItem = Instantiate(_inventoryItemPrefab, _scrollRect.content);
            invItem.Init(item);
        }
    }
}