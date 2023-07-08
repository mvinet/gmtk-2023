using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorSubStateManager : MonoBehaviour
{
    public static string sceneName = "LevelEditorScene";

    private void Awake()
    {
        ScenesManager.LoadScene(SceneLayer.LevelScene,"Level1");
    }
}
