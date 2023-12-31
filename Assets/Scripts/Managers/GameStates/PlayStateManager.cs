using System.Collections.Generic;
using UnityEngine;


public enum PlayMode
{
    Shop = 0,
    Play = 1,
    End = 2
}

public class PlayStateManager : MonoBehaviour
{
    public static PlayStateManager instance;
    public static string sceneName = "PlayScene";

    public PlayMode currentMode;
    public int currentLevel = 0;

    public List<CatDefinition> catDefinitions;
    public Transform mouseContainer;

    public Cat cat;
    public List<Entity> entities = new();

    public BoxCollider2D playAreaCollider;
    
    public void Awake()
    {
        instance = this;
        currentMode = PlayMode.Shop;
        ScenesManager.LoadScene(SceneLayer.UI, PlayUIManager.sceneName);
        ScenesManager.LoadScene(SceneLayer.UI, ShopManager.sceneName);
    }

    public void ChangeMode()
    {
        ReloadEntities();
        if (currentMode == PlayMode.Shop)
            currentMode = PlayMode.Play;
        else if (currentMode == PlayMode.Play)
            currentMode = PlayMode.Shop;

        if (currentMode == PlayMode.Play)
            TriggerManager.OnFightStart.Invoke(new Context());
    }

    public void EndWave()
    {
        TriggerManager.OnFightEnd.Invoke(new Context());
        PlayUIManager.instance.StartShop();
        ChangeMode();
        CurrencyManager.instance.AddCurrency(cat.definition.currencyLoot);
        StartNewWave();
        ShopManager.instance.OnFightRoundEnd();
    }

    public void ReloadEntities()
    {
        foreach (var entity in entities)
        {
            entity.ReloadDefinition();
        }
    }

    private void StartNewWave()
    {
        currentLevel++;
        if (currentLevel == catDefinitions.Count)
        {
            EndManager.message = "Victory !";
            ScenesManager.LoadScene(SceneLayer.UI, EndManager.sceneName);
        }
        else
        {
            cat.Init(catDefinitions[currentLevel]);
        }
    }

    public void OnEntityDeath(Entity deadEntity)
    {
        entities.Remove(deadEntity);
        if (!entities.Find(e => e.GetType() == typeof(Mouse)))
        {
            EndManager.message = "Game Over";
            ScenesManager.LoadScene(SceneLayer.UI, EndManager.sceneName);
        }
    }
}