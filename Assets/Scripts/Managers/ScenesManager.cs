using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public enum SceneLayer
{
    StandAlone = 2,
    GameState = 1, //MENU OU GAME
    UI = 0
}

public class ScenesManager
{
    private static Dictionary<SceneLayer, string> openedScenes = new();

    private static bool isLayerUnique(SceneLayer layer)
    {
        switch (layer)
        {
            case SceneLayer.StandAlone:
                return false;
            case SceneLayer.GameState:
                return true;
            case SceneLayer.UI:
                return false;
            default:
                throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
        }
    }

    public static void LoadScene(SceneLayer sceneLayer, string sceneName)
    {
        if (isLayerUnique(sceneLayer))
        {
            for (int i = 0; i <= (int)sceneLayer; i++)
            {
                var currentLayer = (SceneLayer)i;
                if (openedScenes.TryGetValue(currentLayer, out var scene))
                {
                    UnloadScene(scene);
                }
            }
            

            openedScenes[sceneLayer] = sceneName;
        }
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private static void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}