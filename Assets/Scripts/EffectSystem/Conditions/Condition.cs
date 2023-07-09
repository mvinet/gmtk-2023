using UnityEngine;

//[CreateAssetMenu(fileName = "NewCondition", menuName = "Boss/BossSpell")]
public abstract class Condition : ScriptableObject
{
    public abstract bool ShouldTrigger(Context context);
}