using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public static string sceneName = "EndScene";
    // Start is called before the first frame update


    public static void Restart()
    {
        ScenesManager.LoadScene(SceneLayer.GameState, PlayStateManager.sceneName);
    }
}
