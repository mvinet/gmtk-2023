using System.Linq;
using UnityEngine;


public class BorderManager : MonoBehaviour
{
    public static BorderManager Instance;
    public static BorderDefinition[] AllBorders;
    public Sprite defaultBorder;

    private void Awake()
    {
        AllBorders = Resources.LoadAll<BorderDefinition>("Data/Borders");
        Instance = this;
    }

    public Sprite GetBorderForRarity(Rarity rarity)
    {
        var borderForRarity = AllBorders.First(border => border.rarity == rarity);
        if (borderForRarity == null)
        {
            return defaultBorder;
        }

        return borderForRarity.border;
    }
}