using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effects/DealDamage")]
public class DealDamageEffect : Effect
{
    public int value;
    public override void Apply(Context context)
    {
        context.value = value;
        context.target.DealDamage(value, context);
    }
}