using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;


public class ShopItem : MonoBehaviour
{
    private MouseDefinition _def;
    private GameObject _slotPrefab;

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
        var image = GetComponentInChildren<Image>();
        image.sprite = GetPicto();
        // Todo find how to add TextMeshProUGUI
        // _textPrice = GetComponentInChildren<TextMeshProUGUI>();
        // _textPrice.text = $"{GetPrice()}";
    }
}