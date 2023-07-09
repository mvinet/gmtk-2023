using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class EntityDefinition : ScriptableObject
{
    public int hp;
    public float moveSpeed;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    public string entityName;
    public string description;

    public List<PassiveDefinition> passives;
}
