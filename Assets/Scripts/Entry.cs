using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class Entry : MonoBehaviour
{
    private NetworkRunner runner;
    
#if UNITY_SERVER
    private async void Start()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        var startArgs = new StartGameArgs()
        {
            GameMode = GameMode.Server,
            SessionName = "EternalReturn",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };
        
        await runner.StartGame(startArgs);
    }
#else
    private async void Start()
    {
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;
        
        var sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = "EternalReturn",
            Scene = sceneRef,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        var result = await runner.StartGame(startGameArgs);

        if (!result.Ok)
        {
            Debug.LogError("서버 접속 실패: " + result.ShutdownReason);
        }
    }

#endif   
}
