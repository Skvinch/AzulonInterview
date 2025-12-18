using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image _icon; // UI image used to display the item icon
    [SerializeField] private TextMeshProUGUI _nameText; // UI text used to display the item name
    [SerializeField] private Button _useButton; // Button used to trigger item usage
    
    private ShopItemData _data; // Reference to the item data represented by this UI element

    // Initializes the inventory UI item with data
    public void Init(ShopItemData itemData)
    {
        _data = itemData;

        // Set icon and name from item data
        _icon.sprite = _data.ItemIcon;
        _nameText.text = _data.ItemName;
    }

    // Called when the Use button is pressed
    public void Use()
    {
        // Notify the Inventory that this item was used
        Inventory.Instance.UseItem(_data);
    }
}