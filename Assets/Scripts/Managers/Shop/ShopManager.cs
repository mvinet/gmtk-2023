using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class ShopManager : MonoBehaviour
{
    public int shopRefreshCost;
    public int shopSize;
    public MouseDefinition[] allMice;
    public MouseDefinition[] shopContent;
    public CurrencyManager currencyManager;
    
    
    private void Awake()
    {
        allMice = Resources.LoadAll<MouseDefinition>("Data/Entities/Mice");
        shopContent = GetRandomShopContent();
    }

    private MouseDefinition[] GetRandomShopContent()
    {
        var shopContent = new MouseDefinition[shopSize];
        for (int slot = 0; slot < shopSize; slot++)
        {
            var rarityScore = Random.Range(0, allMice.Sum(mouse => (int)mouse.rarity));
            var rarity = getRarityForRarityScore(rarityScore);
            shopContent[slot] = getMouseMatchingRarity(rarity);
        }

        return shopContent;
    }

    private MouseDefinition getMouseMatchingRarity(Rarity rarity)
    {
        var randomizer = new System.Random();
        var randomizedList = allMice.OrderBy(_ => randomizer.Next()).ToList();
        return randomizedList
            .FirstOrDefault(md => md.rarity == rarity);
    }

    private Rarity getRarityForRarityScore(int rarityScore)
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
        shopContent = GetRandomShopContent();
    }

    public void refreshShop()
    {
        if (currencyManager.hasEnoughCurrency(shopRefreshCost))
        {
            shopContent = GetRandomShopContent();
            currencyManager.useCurrency(shopRefreshCost);
        } // else display error ? Disable refresh button if available currency < shopRefreshCost
        
    }
}
