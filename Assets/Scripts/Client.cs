using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    private NetworkRunner runner;

    private async void OnEnable()
    {
        // NetworkRunner 인스턴스 생성 및 입력 제공 설정
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true; // 클라이언트에서 입력을 제공
        
        var sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);

        // StartGameArgs 설정
        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Client, // 클라이언트 모드
            SessionName = "EternalReturn",   // 서버와 동일한 세션(방) 이름
            Scene = sceneRef, // 현재 씬 인덱스 (클라이언트는 무시됨)
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>() // 씬 매니저
        };

        // 서버에 접속 시도
        var result = await runner.StartGame(startGameArgs);

        if (!result.Ok)
        {
            Debug.LogError("서버 접속 실패: " + result.ShutdownReason);
        }
    }
}