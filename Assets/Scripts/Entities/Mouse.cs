using System.Collections;
using UnityEngine;

public class Mouse : Entity<MouseDefinition>
{
    private Animator _animator;

    public override void Start()
    {
        base.Start();
        Init(definition);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        target = PlayStateManager.instance.cat;
    }

    public override void ReloadDefinition()
    {
        base.ReloadDefinition();
        currentCooldown = 0;
        _spriteRenderer.color = definition.color;
    }

    public override void Die()
    {
        base.Die();
        PlayStateManager.instance.OnEntityDeath(this);
        
        Debug.Log(name + " is dead, RIP IN PEACE LIttlE SOURIS");
        _animator.Play("mouse-death");
    }

    public override void Attack()
    {
        base.Attack();
        _animator.Play("mouse-attack");
    }

    public void OnDeathEvent()
    {
        Destroy(gameObject);
    }
}