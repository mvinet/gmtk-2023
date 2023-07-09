using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnMouseEffect", menuName = "Effects/SpawnMouse")]
public class SpawnMouseEffect : Effect
{
    public Mouse mousePrefab;
    public List<MouseDefinition> mousePool;
    public override void Apply(Context context)
    {
        if (mousePool.Count == 0)
            return;
        var position = context.source.transform.position;
        var mouse = mousePool[Random.Range(0, mousePool.Count)];
        Mouse newMouse = Instantiate(mousePrefab, position // REMPLACER
            , Quaternion.identity, PlayStateManager.instance.mouseContainer);
        
        newMouse.Init(mouse);
    }
}