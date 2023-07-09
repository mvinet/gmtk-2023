using UnityEngine;

[CreateAssetMenu(fileName = "HolderIsTarget", menuName = "Conditions/HolderIsTarget")]
public class HolderIsTargetCondition : Condition
{
    public override bool ShouldTrigger(Context context)
    {
        return context.passiveHolder == context.target ;
    }
}