using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int availableCurrency;
    public TextMeshProUGUI displayedCurrency;

    private void Awake()
    {
        RefreshTextUi();
    }

    public void AddCurrency(int lootedMoney)
    {
        availableCurrency += lootedMoney;
        RefreshTextUi();
    }

    public bool HasEnoughCurrency(int requiredBudget)
    {
        return availableCurrency >= requiredBudget;
    }

    public bool UseCurrency(int cost)
    {
        if (cost > availableCurrency)
        {
            return false;
        }

        availableCurrency -= cost;
        RefreshTextUi();
        return true;
    }

    private void RefreshTextUi()
    {
        displayedCurrency.text = availableCurrency.ToString();
    }
    
}
