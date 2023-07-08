using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "entity/",fileName = "")]
public abstract class EntityDefinition : ScriptableObject
{
    public int hp;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    
}
