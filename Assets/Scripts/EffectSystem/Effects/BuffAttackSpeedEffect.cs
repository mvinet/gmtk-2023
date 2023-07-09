using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffAttackSpeedEffect", menuName = "Effects/ModifyHaste")]
public class BuffAttackSpeedEffect : Effect
{
    public int value;

    public override void Apply(Context context)
    {
        context.target.currentAttackSpeed *= 100f/(100 + value);
    }
}