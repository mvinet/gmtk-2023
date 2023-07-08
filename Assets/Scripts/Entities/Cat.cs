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

    public override void Start()
    {
        base.Start();
        health = PlayUIManager.instance.healthBar;
    }

    private void FixedUpdate()
    {
        if (PlayStateManager.instance.currentMode != PlayMode.Play)
            return;
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
