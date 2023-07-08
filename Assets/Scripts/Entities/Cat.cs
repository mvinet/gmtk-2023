using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Entity<CatDefinition>
{
    private void Awake()
    {
        Init(this.definition);
    }
}
