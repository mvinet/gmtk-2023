using UnityEngine;

public class Scrolls : MonoBehaviour
{
    [SerializeField]
    private GameObject scrolls;
    
    [SerializeField]
    private int speed = 5;
    
    private void FixedUpdate()
    {
        scrolls.transform.localPosition = new Vector2(Time.time * -speed * 10, 0f);
    }
}