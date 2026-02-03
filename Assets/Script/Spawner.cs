using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private NetworkRunner _runner;

    async void StartGame(GameMode mode)
    {
        // Create the Fusion Runner
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        // Start the game session
        // Change this part in your StartGame method:
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestRoom",
            // Use this helper to get the scene info correctly for Fusion 2
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host")) StartGame(GameMode.Host);
            if (GUI.Button(new Rect(0, 40, 200, 40), "Join")) StartGame(GameMode.Client);
        }
    }
}