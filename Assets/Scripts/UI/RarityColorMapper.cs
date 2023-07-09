using System;
using UnityEngine;

namespace UI
{
    public class RarityColorMapper
    {
        public static Color getColorForRarity(Rarity rarity)
        {
            Color targetColor;
            if (ColorUtility.TryParseHtmlString(getHexColorString(rarity), out targetColor))
            {
                return targetColor;
            }

            return Color.white;
        }

        private static String getHexColorString(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.NORMAL:
                    return "#ffffff";
                    
                case Rarity.RARE:
                    return "#00c4ff";
                case Rarity.EPIC:
                    return "#c07cff";
                case Rarity.LEGENDARY:
                    return "#fbff00";
                default:
                    throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null);
            }
        }
    }
}