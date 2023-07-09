using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum Trigger
{
    Never,
    OnFightStart,
    OnAttack,
    EveryTick,
    OnDamageReceived,
    OnDeath,
    OnFightEnd,
}
public struct Context
{
    public Entity source;
    public Entity target;
    public Entity passiveHolder;
    public int value;
}

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance;

    public static UnityEvent<Context> Never = new UnityEvent<Context>();
    public static UnityEvent<Context> OnFightStart = new UnityEvent<Context>();
    public static UnityEvent<Context> OnAttack = new UnityEvent<Context>();
    public static UnityEvent<Context> EveryTick = new UnityEvent<Context>();
    public static UnityEvent<Context> OnDamageReceived = new UnityEvent<Context>();
    public static UnityEvent<Context> OnDeath = new UnityEvent<Context>();
    public static UnityEvent<Context> OnFightEnd = new UnityEvent<Context>();

    public static Dictionary<Trigger, UnityEvent<Context>> triggerMap = new Dictionary<Trigger, UnityEvent<Context>>
    {
        { Trigger.Never,Never},
        { Trigger.OnFightStart,OnFightStart},
        { Trigger.OnAttack,OnAttack},
        { Trigger.EveryTick,EveryTick},
        { Trigger.OnDamageReceived,OnDamageReceived},
        { Trigger.OnDeath,OnDeath},
        { Trigger.OnFightEnd,OnFightEnd},
    };
    private float timerTick = 0.1f;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if(PlayStateManager.instance.currentMode == PlayMode.Play)
        {
            timerTick -= Time.deltaTime;
            while(timerTick<0)
            {
                timerTick += 0.1f;
                triggerMap[Trigger.EveryTick].Invoke(new Context());
            }
        }
    }
}
