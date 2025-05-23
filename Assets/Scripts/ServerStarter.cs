using UnityEngine;
using Fusion;

public class ServerStarter : MonoBehaviour
{
    private NetworkRunner runner;

    async void Start()
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
}
