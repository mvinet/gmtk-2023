using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int availableCurrency;
    public TextMeshProUGUI displayedCurrency;

    private void Awake()
    {
        instance = this;
        displayedCurrency.text = availableCurrency.ToString();
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
