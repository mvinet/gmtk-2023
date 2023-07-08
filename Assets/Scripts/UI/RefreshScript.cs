using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RefreshScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject button;
    public Sprite refreshPressed;
    public Sprite refreshReleased;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        button.GetComponent<Image>().sprite = refreshPressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        button.GetComponent<Image>().sprite = refreshReleased;
    }
}
