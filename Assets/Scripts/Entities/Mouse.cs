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

    public override void Die()
    {
        base.Die();
        PlayStateManager.instance.entities.Remove(this);
        Debug.Log(name + " is dead, RIP IN PEACE LIttlE SOURIS");
        _animator.Play("mouse-death");
    }

    public override void Attack()
    {
        if(currentHp <= 0) return;
        
        base.Attack();
        _animator.Play("mouse-attack");
    }

    public void OnDeathEvent()
    {
        Destroy(gameObject);
    }
}