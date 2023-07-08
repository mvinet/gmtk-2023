using TMPro;
using UnityEngine;

namespace UI
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager _instance;

        public TextMeshProUGUI textComponent;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
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

        public void SetAndShowTooltip(string message)
        {
            gameObject.SetActive(true);
            textComponent.text = message;
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
            textComponent.text = string.Empty;
        }
    }
}