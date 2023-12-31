using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;

        public TextMeshProUGUI headerField;
        public TextMeshProUGUI contentField;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.position = Input.mousePosition;
        }

        public void SetEntityDefinitionTooltip(EntityDefinition definition)
        {
            gameObject.SetActive(true);

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
            content.Add("Speed : " + definition.moveSpeed);

            contentField.text = string.Join("\n", content);

            headerField.text = definition.entityName;

            if (definition is MouseDefinition mouseDefinition)
            {
                headerField.color = RarityColorMapper.getColorForRarity(mouseDefinition.rarity);
            }
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
            headerField.text = string.Empty;
        }
    }
}