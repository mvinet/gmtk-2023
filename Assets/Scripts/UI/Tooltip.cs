using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Tooltip : MonoBehaviour
    {
        public EntityDefinition definition;

        private void OnMouseEnter()
        {
            var content = new List<string>();

            if (definition.description != "")
            {
                content.Add(definition.description);
                content.Add("");
            }

            content.Add("Health : " + definition.hp);
            content.Add("Attack Damage : " + definition.attackDamage);
            content.Add("Attack Speed : " + definition.attackSpeed);
            content.Add("Attack Range : " + definition.attackRange);
            content.Add("Attack Damage : " + definition.hp);
            content.Add("Speed : " + definition.moveSpeed);

            TooltipManager.Instance.SetAndShowTooltip(definition.mouseName, content.ToArray());
        }

        private void OnMouseExit()
        {
            TooltipManager.Instance.HideTooltip();
        }
    }
}