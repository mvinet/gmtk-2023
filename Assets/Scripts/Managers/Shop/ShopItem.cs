using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ShopItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private MouseDefinition _def;
    private GameObject _slotPrefab;
    private Vector3 posBeforeDrag;
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


    public void OnPointerDown(PointerEventData eventData)
    {
        if (! ShopManager.INSTANCE.CanBuyMouse(_def))
        {
            //playSound(NO)
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (ShopManager.INSTANCE.CanBuyMouse(_def)) {
            ShopManager.INSTANCE.BuyMouse(_def);    
        } else
        {
            gameObject.transform.position =  posBeforeDrag;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        posBeforeDrag = transform.position; // Save starting position
    }
}