using System;
using UnityEngine;
using UnityEngine.UI;

public class Cat : Entity<CatDefinition>
{
    [SerializeField]
    private Image health;
    
    private void Awake()
    {
        Init(definition);
    }

    private void Start()
    {
        health = PlayUIManager.instance.healthBar;
        PlayStateManager.instance.entities.Add(this);
    }

    private void FixedUpdate()
    {
        health.fillAmount = currentHp / (float) definition.hp;
        
        var pos = transform.position;
        var minDistance = float.PositiveInfinity;

        foreach (var obj in GameObject.FindGameObjectsWithTag("Mouse"))
        {
            var distance = (pos - obj.transform.position).sqrMagnitude;
            if (distance >= minDistance) continue;

            minDistance = distance;
            target = obj.GetComponent<Mouse>();
        }

        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            Time.deltaTime * moveSpeed
        );
    }

    public override void Die()
    {
        Debug.Log("Cat is dead");
        PlayStateManager.instance.EndWave();
    }
}
