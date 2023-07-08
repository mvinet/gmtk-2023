using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneLayer
{
    StandAlone = 0,
    GameState = 1, //MENU OU GAME
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
            default:
                throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
        }
    }

    public static void LoadScene(SceneLayer sceneLayer, string sceneName)
    {
        if (isLayerUnique(sceneLayer))
        {
            if (openedScenes.TryGetValue(sceneLayer, out var scene))
            {
                UnloadScene(scene);
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