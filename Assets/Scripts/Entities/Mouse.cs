using System.Collections;
using UnityEngine;

public class Mouse : Entity<MouseDefinition>
{
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("Death");

    private void Start()
    {
        Init(definition);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        target = PlayStateManager.instance.cat;
    }

    private void FixedUpdate()
    {
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            Time.deltaTime * moveSpeed
        );

        _spriteRenderer.flipX = target.transform.position.x < transform.position.x;
    }

    public override void Die()
    {
        Debug.Log(name + " is dead, RIP IN PEACE LIttlE SOURIS");
        _animator.ResetTrigger(Attack1);
        _animator.SetTrigger(Death);
    }

    public override void Attack()
    {
        if(currentHp <= 0) return;
        
        base.Attack();
        _animator.SetTrigger(Attack1);
    }

    public void OnDeathEvent()
    {
        Destroy(gameObject);
    }
}