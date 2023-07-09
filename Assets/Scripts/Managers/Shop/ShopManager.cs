using System;
using System.Collections.Generic;
using System.Linq;
using Managers.Shop;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class ShopManager : MonoBehaviour
{
    public static string sceneName = "ShopScene";
    public static ShopManager instance;
    public int shopRefreshCost;
    public int shopSize;
    public MouseDefinition[] allMice;
    public CurrencyManager currencyManager;
    public GameObject shopUi;
    public GameObject shopSlotPrefab;
    private List<MouseDefinition> _currentShopContent;
    public Mouse mousePrefab;
    public SceneAsset playScene;


    private void Awake()
    {
        instance = this;
        allMice = Resources.LoadAll<MouseDefinition>("Data/Entities/Mice");
        _currentShopContent = GetRandomShopContent();
        // initialize UI
        GenerateAllShopSlots();
    }

    private void GenerateAllShopSlots()
    {
        for (int i = 0; i < shopSize; i++)
        {
            var slot = Instantiate(shopSlotPrefab, shopUi.transform, true);
            slot.GetComponent<ShopItem>().SetMouseDefinition(_currentShopContent[i]);
            slot.GetComponent<ShopItem>().Refresh();
        }

        instance = this;
    }

    private void DestroyAllRemainingShopSlots()
    {
        var componentsInChildren = shopUi.GetComponentsInChildren<ShopItem>();
        foreach (var component in componentsInChildren)
        {
            Destroy(component.gameObject);
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
        RefreshShop();
        currencyManager.AddCurrency(shopRefreshCost);
    }

    public void RefreshShop()
    {
        if (currencyManager.HasEnoughCurrency(shopRefreshCost))
        {
            DestroyAllRemainingShopSlots();
            GenerateAllShopSlots();
            _currentShopContent = GetRandomShopContent();
            currencyManager.UseCurrency(shopRefreshCost);
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
                i %= shopItems.Length;
            }

            shopItems[i].SetMouseDefinition(mouse);
            shopItems[i].Refresh();
            i++;
        }
    }

    public bool BuyMouse(ShopItem shopItem, Vector2 whereToSpawn)
    {
        if (!PlayStateManager.instance.playAreaCollider.bounds.Contains(whereToSpawn))
            return false;
        Mouse newMouse = Instantiate(mousePrefab, whereToSpawn
            , Quaternion.identity, PlayStateManager.instance.mouseContainer);

        newMouse.Init(shopItem.GetMouseDefinition());

        currencyManager.UseCurrency(shopItem.GetMouseDefinition().price);
        return true;
    }

    public bool CanBuyMouse(MouseDefinition def)
    {
        return currencyManager.HasEnoughCurrency(def.price);
    }
}