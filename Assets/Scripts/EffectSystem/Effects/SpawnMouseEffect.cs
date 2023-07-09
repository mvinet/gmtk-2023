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
        var spawnPosition = new Vector3(position.x += Random.Range(-.5f, .5f), position.y += Random.Range(-.5f, .5f),
            position.z);
        var mouse = mousePool[Random.Range(0, mousePool.Count)];
        Mouse newMouse = Instantiate(mousePrefab, spawnPosition
            , Quaternion.identity, PlayStateManager.instance.mouseContainer);
        
        newMouse.Init(mouse);
    }
}