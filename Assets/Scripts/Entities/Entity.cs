using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity : MonoBehaviour
{
    public Vector2 initialPosition;
    public int currentHp;
    public int currentAttackDamage;
    public float currentCooldown;
    public float currentAttackSpeed;
    public float currentAttackRange;
    public float moveSpeed;

    public Entity target;


    public virtual void Start()
    {
        PlayStateManager.instance.entities.Add(this);
    }

    private void Update()
    {
        if (PlayStateManager.instance.currentMode != PlayMode.Play)
            return;
        currentCooldown -= Time.deltaTime;
        if (target == null)
            return;
        
        if (Vector2.Distance(transform.position, target.transform.position) <= currentAttackRange &&
            currentCooldown <= 0)
        {
            currentCooldown = 1 / currentAttackSpeed;
            Attack();
        }
    }

    public virtual void Attack()
    {
    }

    public void DealDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }
    public virtual void ReloadDefinition(){}

    public virtual void Die()
    {
    }
}

public abstract class Entity<T> : Entity where T : EntityDefinition
{
    public T definition;


    public void Init(T definition)
    {
        this.definition = definition;
        initialPosition = new Vector2(transform.position.x,transform.position.y);
        ReloadDefinition();
    }

    public override void ReloadDefinition()
    {
        transform.position = initialPosition;
        currentHp = definition.hp;
        currentAttackDamage = definition.attackDamage;
        currentCooldown = 0f;
        currentAttackRange = definition.attackRange;
        moveSpeed = definition.moveSpeed;
        currentAttackSpeed = definition.attackSpeed;
    }


    public override void Attack()
    {
        target.DealDamage(currentAttackDamage);
    }
}