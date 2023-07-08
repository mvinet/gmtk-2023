using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStateManager : MonoBehaviour
{
    public static string sceneName = "PlayScene";
    
    private void Awake()
    {
        ScenesManager.LoadScene(SceneLayer.SubGameState,LevelEditorSubStateManager.sceneName);
    }
    
    
}
