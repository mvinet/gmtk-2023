using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Target", menuName = "TargetSelector/Target")]
public class TargetTargetSelector : TargetSelector
{
    
    public override List<Entity> GetTargets(Context context)
    {
        return new List<Entity>() { context.target };
    }
}