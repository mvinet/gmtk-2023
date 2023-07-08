using System;
using System;
using System.Linq;
using UnityEngine;

public class Cat : Entity<CatDefinition>
{
    private void Awake()
    {
        Init(this.definition);
    }
    
    private void FixedUpdate()
    {
        var pos = transform.position;
        var minDistance = float.PositiveInfinity;
        GameObject target = null;
        
        foreach(var obj in GameObject.FindGameObjectsWithTag("Mouse"))
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
}
