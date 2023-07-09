using System;
using TMPro;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public static string sceneName = "EndScene";

    public static string message = "Game Over";

    public TextMeshProUGUI TextMeshPro;

    private void Start()
    {
        TextMeshPro.text = message;
    }

    public static void Restart()
    {
        ScenesManager.LoadScene(SceneLayer.GameState, PlayStateManager.sceneName);
    }
}
