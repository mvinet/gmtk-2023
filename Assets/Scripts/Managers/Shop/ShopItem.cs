using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;


public class ShopItem : MonoBehaviour
{
    private MouseDefinition _def;
    private GameObject _slotPrefab;
    private ShopManager _shopManager;
    public void SetMouseDefinition(MouseDefinition def)
    {
        _def = def;
    }
    
    public String GetPrice()
    {
        return ((int)_def.rarity).ToString();
    }

    public Sprite GetPicto()
    {
        return _def.picto;
    }
    
    public void Refresh()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        GetComponentInChildren<Image>().sprite = GetPicto();
        GetComponentInChildren<TextMeshProUGUI>().text = GetPrice();
    }
}