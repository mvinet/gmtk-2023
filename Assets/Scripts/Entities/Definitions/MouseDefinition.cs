using UnityEngine;

[CreateAssetMenu(menuName = "entity/mouse",fileName = "MouseDefinition")]
public class MouseDefinition : EntityDefinition
{
    public Rarity rarity;
    public Sprite picto;
    public int price = 1;
}
