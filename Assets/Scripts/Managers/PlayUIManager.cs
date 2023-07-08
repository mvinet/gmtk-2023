using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public static PlayUIManager instance;
    public Image healthBar;
    public static string sceneName = "PlayUiScene";


    private void Awake()
    {
        instance = this;
    }
}
