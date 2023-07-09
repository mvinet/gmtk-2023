﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HolderIsSourceCondition", menuName = "Conditions/HolderIsSource")]
public class HolderIsSourceCondition : Condition
{
    public override bool ShouldTrigger(Context context)
    {
        return context.passiveHolder == context.source ;
    }
}