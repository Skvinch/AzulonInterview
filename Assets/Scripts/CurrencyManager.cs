using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; } // Singleton instance for global access
    public int Coins { get; private set; } // Current amount of coins the player has
    public event Action<int> OnCoinsChanged; // Event invoked whenever the coin amount changes

    [SerializeField] private int _startingCoins; // Initial amount of coins set from the Inspector

    private void Awake()
    {
        // Ensure only one instance of CurrencyManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Initialize coins with the starting value
        Coins = _startingCoins;
    }

    // Checks if the player has enough coins to afford a cost
    public bool CanAfford(int amount)
    {
        return Coins >= amount;
    }

    // Tries to spend the given amount of coins
    public bool SpendCoins(int amount)
    {
        // If not enough coins, cancel the transaction
        if (!CanAfford(amount))
            return false;

        // Deduct coins and notify listeners
        Coins -= amount;
        OnCoinsChanged?.Invoke(Coins);

        return true;
    }
}