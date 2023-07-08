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
            TooltipManager._instance.SetAndShowTooltip(definition.name);
        }

        private void OnMouseExit()
        {
            TooltipManager._instance.HideTooltip();
        }
    }
}
