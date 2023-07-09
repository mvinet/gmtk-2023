using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AllMice", menuName = "TargetSelector/AllMice")]
public class AllMiceTargetSelector : TargetSelector
{
    public override List<Entity> GetTargets(Context context)
    {
        var mice = new List<Entity>(from mouse in PlayStateManager.instance.entities where mouse is Mouse select mouse);
        return mice;
    }
}