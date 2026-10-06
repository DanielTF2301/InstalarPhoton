using UnityEngine;
using Fusion;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    
    public GameObject playerPrefab;

    //If "MY" player enters the game, show them
    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, new Vector3(Random.Range(-5,5),1,0), Quaternion.identity);
        }
    }

}
