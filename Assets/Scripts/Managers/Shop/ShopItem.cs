using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers.Shop
{
    public class ShopItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        private MouseDefinition _def;
        private Vector3 _posBeforeDrag;
        public Image borderImage;
        public Image shopItemImage;

        public MouseDefinition GetMouseDefinition()
        {
            return _def;
        }
        public void SetMouseDefinition(MouseDefinition def)
        {
            _def = def;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public String GetDisplayablePrice()
        {
            return _def.price.ToString();
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

            shopItemImage.sprite = GetPicto();
            shopItemImage.color = _def.color;
            GetComponentInChildren<TextMeshProUGUI>().text = GetDisplayablePrice();
            borderImage.sprite = BorderManager.Instance.GetBorderForRarity(_def.rarity);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (ShopManager.instance.CanBuyMouse(_def) && PlayStateManager.instance.currentMode == PlayMode.Shop)
            {
                var worldCoordinate = Camera.main.ScreenToWorldPoint(eventData.position);
                // Buy will : Use currency, instantiate mouse in the play scene

                if (ShopManager.instance.BuyMouse(this, worldCoordinate))
                {
                    Destroy(gameObject);    
                }
                //This will destroy the shop item being dragged
                
                
            }
            // if the action was not performed properly, snapback shop item in its original place
            RevertPosition();
        }

        public void RevertPosition()
        {
            gameObject.transform.position = _posBeforeDrag;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!ShopManager.instance.CanBuyMouse(_def) || PlayStateManager.instance.currentMode != PlayMode.Shop )
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