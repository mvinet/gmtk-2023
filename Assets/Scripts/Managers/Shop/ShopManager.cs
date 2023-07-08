using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ShopManager : MonoBehaviour
{
    public int shopRefreshCost;
    public int shopSize;
    public MouseDefinition[] allMice;
    public CurrencyManager currencyManager;
    public GameObject shopUi;
    public GameObject shopSlotPrefab;
    private List<MouseDefinition> _currentShopContent;
    
    private void Awake()
    {
        allMice = Resources.LoadAll<MouseDefinition>("Data/Entities/Mice");
        var chosenMice = GetRandomShopContent();
        refreshShopUi(chosenMice);
    }

    private List<MouseDefinition> GetRandomShopContent()
    {
        var mouseSelection = new List<MouseDefinition>();
        for (int slot = 0; slot < shopSize; slot++)
        {
            var rarityScore = Random.Range(0, allMice.Sum(mouse => (int)mouse.rarity));
            var rarity = GetRarityForRarityScore(rarityScore);
            mouseSelection.Add(GetMouseMatchingRarity(rarity));
        }

        return mouseSelection;
    }

    private MouseDefinition GetMouseMatchingRarity(Rarity rarity)
    {
        var randomizer = new System.Random();
        var randomizedList = allMice.OrderBy(_ => randomizer.Next()).ToList();
        return randomizedList
            .FirstOrDefault(md => md.rarity == rarity);
    }

    private Rarity GetRarityForRarityScore(int rarityScore)
    {
        int localAccumulator = 0;
        foreach (Rarity rarity in Enum.GetValues(typeof(Rarity)))
        {
            localAccumulator += (int)rarity;
            if (localAccumulator >= rarityScore)
            {
                return rarity;
            }
        }

        return Rarity.NORMAL;
    }

    public void onFightRoundEnd()
    {
        _currentShopContent = GetRandomShopContent();
        refreshShopUi(_currentShopContent);
    }

    public void refreshShop()
    {
        if (currencyManager.hasEnoughCurrency(shopRefreshCost))
        {
            _currentShopContent = GetRandomShopContent();
            currencyManager.useCurrency(shopRefreshCost);
            refreshShopUi(_currentShopContent);
        } // else display error ? Disable refresh button if available currency < shopRefreshCost
        
    }

    public void refreshShopUi(List<MouseDefinition> definitions)
    {
        var shopBar = shopUi.GetComponent<Image>().transform;
        foreach (var shopDefinition in definitions)
        {
            var slot = Instantiate(shopSlotPrefab, shopBar, true);
            slot.GetComponent<ShopItem>().SetMouseDefinition(shopDefinition);
            slot.GetComponent<ShopItem>().Refresh();
        }
    }

    public void DebugShopContent()
    {
        Debug.Log(_currentShopContent);
    }
}
