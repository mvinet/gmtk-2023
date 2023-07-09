using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "RandomMouseTargetSelector", menuName = "TargetSelector/RandoMouse")]
public class RandomMouseTargetSelector : TargetSelector
{
    public override List<Entity> GetTargets(Context context)
    {
        var mice = new List<Entity>(from mouse in PlayStateManager.instance.entities where mouse is Mouse  && mouse != context.source select mouse);
        return mice.Count > 0 ? new List<Entity> { mice[Random.Range(0, mice.Count)] } : new List<Entity>();
    }
}