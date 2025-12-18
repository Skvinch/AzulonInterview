using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    // Reference to the UI text that displays the coin amount
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Start()
    {
        // Initialize the coin text with the current amount of coins
        UpdateText(CurrencyManager.Instance.Coins);

        // Subscribe to the coin change event to update UI automatically
        CurrencyManager.Instance.OnCoinsChanged += UpdateText;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        CurrencyManager.Instance.OnCoinsChanged -= UpdateText;
    }

    // Updates the coin text whenever the coin value changes
    private void UpdateText(int value)
    {
        _coinText.text = value.ToString();
    }
}