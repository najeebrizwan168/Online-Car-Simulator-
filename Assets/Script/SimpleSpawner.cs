using Fusion;
using UnityEngine;

public class SimpleSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab; // Drag your McLaren Prefab here

    public void PlayerJoined(PlayerRef player)
    {
        // Only spawn the car for the local player who just joined
        if (player == Runner.LocalPlayer)
        {
            // Spawns the car at the center of the scene
            Runner.Spawn(PlayerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player);
            Debug.Log("McLaren Spawned for Player: " + player);
        }
    }
}