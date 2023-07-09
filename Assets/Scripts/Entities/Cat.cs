using System;
using UnityEngine;
using UnityEngine.UI;

public class Cat : Entity<CatDefinition>
{
    [SerializeField]
    private Image health;

    private Animator _animator;
    
    private void Awake()
    {
        Init(definition);
    }

    public override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        health = PlayUIManager.instance.healthBar;
    }

    public override void Update()
    {
        base.Update();
        health.fillAmount = currentHp / (float) definition.hp;
    }

    public override void FixedUpdate()
    {
        FindTarget();
        base.FixedUpdate();
    }

    public void FindTarget()
    {
        var pos = transform.position;
        var minDistance = float.PositiveInfinity;

        foreach (var obj in GameObject.FindGameObjectsWithTag("Mouse"))
        {
            var distance = (pos - obj.transform.position).sqrMagnitude;
            if (distance >= minDistance) continue;

            minDistance = distance;
            target = obj.GetComponent<Mouse>();
        }
    }

    public override void Die()
    {
        Debug.Log("Cat is dead");
        _animator.Play("cat-death");
    }
    
    public override void Attack()
    {
        base.Attack();
        _animator.Play("cat-attack");
    }

    public void OnCatDeathEvent()
    {
        PlayStateManager.instance.EndWave();
    }
}
