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
    private static Dictionary<SceneLayer, List<string>> openedScenes = new();

    private static bool isLayerUnique(SceneLayer layer)
    {
        return layer switch
        {
            SceneLayer.StandAlone => false,
            SceneLayer.GameState => true,
            SceneLayer.UI => false,
            _ => throw new ArgumentOutOfRangeException(nameof(layer), layer, null)
        };
    }

    public static void LoadScene(SceneLayer sceneLayer, string sceneName)
    {
        if (isLayerUnique(sceneLayer))
        {
            for (int i = 0; i <= (int)sceneLayer; i++)
            {
                var currentLayer = (SceneLayer)i;
                UnloadAllLayerScenes(currentLayer);
            }
        }

        if (!openedScenes.ContainsKey(sceneLayer))
            openedScenes[sceneLayer] = new List<string>();
        openedScenes[sceneLayer].Add(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private static void UnloadAllLayerScenes(SceneLayer layer)
    {
        if(openedScenes.TryGetValue(layer, out var list))
        {
            foreach (var scene in list)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
            openedScenes[layer].Clear();
        }
    }
}