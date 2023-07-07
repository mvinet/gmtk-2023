using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneLayer
{
    StandAlone,
    GameState, //MENU OU GAME
    SubGameState, // JEU OU EDITOR
    LevelScene, // NIVEAU QUI SCROLL
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
            case SceneLayer.SubGameState:
            case SceneLayer.LevelScene:
                return true;
            default:
                throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
        }
    }

    public static void LoadScene(SceneLayer sceneLayer, string sceneName)
    {
        if (isLayerUnique(sceneLayer))
        {
            if (openedScenes.ContainsKey(sceneLayer))
            {
                UnloadScene(openedScenes[sceneLayer]);
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