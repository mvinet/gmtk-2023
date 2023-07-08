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

    private void FixedUpdate()
    {
        health.fillAmount = currentHp / (float) definition.hp;
        
        Debug.Log(currentHp / definition.hp);
        
        var pos = transform.position;
        var minDistance = float.PositiveInfinity;
        GameObject target = null;

        foreach (var obj in GameObject.FindGameObjectsWithTag("Mouse"))
        {
            var distance = (pos - obj.transform.position).sqrMagnitude;
            if (distance >= minDistance) continue;

            minDistance = distance;
            target = obj;
        }

        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            Time.deltaTime * moveSpeed
        );
    }

    public void OnDeath()
    {
        PlayStateManager.instance.EndWave();
    }
}
