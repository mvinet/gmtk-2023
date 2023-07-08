using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity<T> : MonoBehaviour where T : EntityDefinition
{
    public T definition;

    public Vector2 initialPosition;
    public int currentHp;
    public int currentAttackDamage;
    public float currentCooldown;
    public float currentAttackRange;
    public float moveSpeed;

    
    public void Init(T definition)
    {
        this.definition = definition;
        initialPosition = transform.position;
        ReloadDefinition();
    }
    public void ReloadDefinition()
    {
        transform.position = initialPosition;
        currentHp = definition.hp;
        currentAttackDamage = definition.attackDamage;
        currentCooldown = 0f;
        currentAttackRange = definition.attackRange;
        moveSpeed = definition.moveSpeed;
    }
}
