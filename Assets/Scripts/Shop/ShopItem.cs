using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image _itemIconImage; // UI image used to display the item icon
    [SerializeField] private TextMeshProUGUI _itemNameText; // UI text used to display the item name
    [SerializeField] private TextMeshProUGUI _itemPriceText; // UI text used to display the item price
    [SerializeField] private Button _buyButton; // Button used to purchase the item
     
    private ShopItemData _data; // Data associated with this shop item
    private Shop _shop; // Reference to the Shop that handles purchasing logic

    // Initializes the shop item UI with item data and shop reference
    public void Init(ShopItemData data, Shop shop)
    {
        _data = data;
        _shop = shop;

        // Set UI visuals from item data
        _itemIconImage.sprite = data.ItemIcon;
        _itemNameText.text = data.ItemName;
        _itemPriceText.text = data.ItemPrice.ToString();

        // Update button state based on inventory status
        UpdateButton();
    }

    // Updates the Buy button interactability
    private void UpdateButton()
    {
        // Disable the button if the item is already owned
        _buyButton.interactable = !Inventory.Instance.HasItem(_data);
    }

    // Called when the Buy button is pressed
    public void Buy()
    {
        // Ask the Shop to attempt purchasing this item
        _shop.TryBuyItem(_data);

        // Refresh button state after purchase attempt
        UpdateButton();
    }
}