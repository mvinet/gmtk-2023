using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers.Shop
{
    public class ShopItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private MouseDefinition _def;
        private GameObject _slotPrefab;
        private Vector3 _posBeforeDrag;

        public void SetMouseDefinition(MouseDefinition def)
        {
            _def = def;
        }

        public String GetPrice()
        {
            return "1";
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


        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (ShopManager.INSTANCE.CanBuyMouse(_def))
            {
                // Buy will : Use currency, instantiate mouse in the play scene
                
                ShopManager.INSTANCE.BuyMouse(_def, Camera.main.ScreenToWorldPoint(eventData.position));
                //This will destroy the shop item being dragged
                Destroy(gameObject);
            }
            else
            {
                gameObject.transform.position = _posBeforeDrag;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!ShopManager.INSTANCE.CanBuyMouse(_def))
            {
                eventData.pointerDrag = null;
            }

            _posBeforeDrag = transform.position; // Save starting position
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.SetEntityDefinitionTooltip(_def);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}