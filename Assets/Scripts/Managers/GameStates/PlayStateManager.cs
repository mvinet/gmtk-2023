using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayMode
{
    Shop = 0,
    Play = 1
}
public class PlayStateManager : MonoBehaviour
{
    public static PlayStateManager instance;
    public static string sceneName = "PlayScene";

    public PlayMode currentMode;

    public List<Entity<EntityDefinition>> entities = new();


    public void Awake()
    {
        instance = this;
        currentMode = PlayMode.Shop;
    }

    public void ChangeMode()
    {
        if (currentMode == PlayMode.Shop)
            currentMode = PlayMode.Play;
        else if (currentMode == PlayMode.Play)
            currentMode = PlayMode.Shop;

        foreach (var entity in entities)
        {
            entity.ReloadDefinition();
        }
        
    }
    
}
