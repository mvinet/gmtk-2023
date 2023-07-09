using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainCheese", menuName = "Effects/GainCheese")]
public class GainCheeseEffect : Effect
{
    public int value;
    public override void Apply(Context context)
    {
        CurrencyManager.instance.AddCurrency(value);
    }
}