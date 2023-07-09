using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "borders/border",fileName = "BorderDefinition")]
public class BorderDefinition : ScriptableObject
{
    public Sprite border;
    public Rarity rarity;
}
