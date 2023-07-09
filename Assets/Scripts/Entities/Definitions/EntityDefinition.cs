using UnityEngine;
using UnityEngine.Serialization;

public abstract class EntityDefinition : ScriptableObject
{
    public int hp;
    public float moveSpeed;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    public string mouseName;
    public string description;

}
