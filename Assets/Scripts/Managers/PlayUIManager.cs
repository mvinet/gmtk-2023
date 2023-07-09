using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public static PlayUIManager instance;
    public Image healthBar;
    public GameObject healthBarContainer;
    public GameObject startFightButton;
    public static string sceneName = "PlayUiScene";


    private void Awake()
    {
        instance = this;
    }

    public void StartFight()
    {
        healthBarContainer.gameObject.SetActive(true);
        startFightButton.SetActive(false);
        PlayStateManager.instance.ChangeMode();
    }

    public void StartShop()
    {
        healthBarContainer.gameObject.SetActive(false);
        startFightButton.SetActive(true);
    }
}