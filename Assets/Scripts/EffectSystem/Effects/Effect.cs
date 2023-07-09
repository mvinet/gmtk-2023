﻿using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Apply(Context context);
}