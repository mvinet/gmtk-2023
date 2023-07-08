using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int availableCurrency;

    public void addCurrency(int lootedMoney)
    {
        availableCurrency += lootedMoney;
    }

    public bool hasEnoughCurrency(int requiredBudget)
    {
        return availableCurrency >= requiredBudget;
    }

    public bool useCurrency(int cost)
    {
        if (cost > availableCurrency)
        {
            Debug.Log("ALLO IL FAUT UTILISER HAS ENOUGH CURRENCY AVANT POUR EVITER DE S'ENDETTER");
            return false;
        }

        availableCurrency -= cost;
        return true;
    }
    
}
