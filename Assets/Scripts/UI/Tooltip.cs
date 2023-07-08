using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class Tooltip : MonoBehaviour
    {
        public EntityDefinition definition;

        private void OnMouseEnter()
        {
            TooltipManager.Instance.SetAndShowTooltip(definition.name, new []
            {
                "It's a bad cat",
                "He keep all the cheese",
                "UwU"
            });
        }

        private void OnMouseExit()
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}
