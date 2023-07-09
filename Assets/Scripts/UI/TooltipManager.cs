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
        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void SetAndShowTooltip(string message, string[] descriptions)
        {
            gameObject.SetActive(true);
            headerField.text = message;

            contentField.text = string.Join("\n", descriptions);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
            headerField.text = string.Empty;
        }
    }
}