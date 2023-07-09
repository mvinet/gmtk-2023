using UnityEngine;


public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ScenesManager.LoadScene(SceneLayer.GameState, PlayStateManager.sceneName);
        
    }
}