using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShopManager : MonoBehaviour
{
    public static string sceneName = "ShopScene";
    public int shopRefreshCost;
    public int shopSize;
    public MouseDefinition[] allMice;
    public CurrencyManager currencyManager;
    public GameObject shopUi;
    public GameObject shopSlotPrefab;
    private List<MouseDefinition> _currentShopContent;
    public Mouse mousePrefab;


    private void Awake()
    {
        allMice = Resources.LoadAll<MouseDefinition>("Data/Entities/Mice");
        _currentShopContent = GetRandomShopContent();
        // initialize UI
        for (int i = 0; i < shopSize; i++)
        {
            var slot = Instantiate(shopSlotPrefab, shopUi.transform, true);
            slot.GetComponent<ShopItem>().SetMouseDefinition(_currentShopContent[i]);
            slot.GetComponent<ShopItem>().Refresh();
        }
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

    public void OnFightRoundEnd()
    {
        _currentShopContent = GetRandomShopContent();
        RefreshShopUi(_currentShopContent);
    }

    public void RefreshShop()
    {
        if (currencyManager.hasEnoughCurrency(shopRefreshCost))
        {
            _currentShopContent = GetRandomShopContent();
            currencyManager.useCurrency(shopRefreshCost);
            RefreshShopUi(_currentShopContent);
        } // else display error ? Disable refresh button if available currency < shopRefreshCost
    }

    public void RefreshShopUi(List<MouseDefinition> definitions)
    {
        var shopItems = shopUi.GetComponentsInChildren<ShopItem>();
        int i = 0;
        foreach (var mouse in definitions)
        {
            if (i > shopItems.Length)
            {
                Debug.Log("On a plus d'objet à ajouter que de place dans le shop");
                i %= shopItems.Length;
            }

            shopItems[i].SetMouseDefinition(mouse);
            shopItems[i].Refresh();
            i++;
        }
    }

    public void DebugShopContent()
    {
        String debugString = "";
        foreach (var md in _currentShopContent)
        {
            debugString += " " + md.name + "| ";
        }

        Debug.Log(debugString);
    }

    public void BuyMouse(MouseDefinition definition)
    {
        Mouse newMouse = Instantiate(mousePrefab, transform.position // REMPLACER
            , Quaternion.identity, PlayStateManager.instance.mouseContainer);
        
        newMouse.Init(definition);
    }
}