using System;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int availableCurrency;
    public TextMeshProUGUI displayedCurrency;

    private void Awake()
    {
        displayedCurrency.text = availableCurrency.ToString();
    }

    public void addCurrency(int lootedMoney)
    {
        availableCurrency += lootedMoney;
        displayedCurrency.text = availableCurrency.ToString();
    }

    public bool hasEnoughCurrency(int requiredBudget)
    {
        return availableCurrency >= requiredBudget;
    }

    public bool useCurrency(int cost)
    {
        if (cost > availableCurrency)
        {
            return false;
        }

        availableCurrency -= cost;
        return true;
    }
    
}
