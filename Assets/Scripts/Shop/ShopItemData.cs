using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Shop/ShopItemData")]
public class ShopItemData : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public int ItemPrice;
    public int EquipSlotId;
}
