using Cinemachine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour , INetworkRunnerCallbacks
{
    public NetworkObject playerPrefab;
    public NetworkObject magicCubePrefab;

    public CinemachineVirtualCamera followCamera;
    public NetworkRunner networkRunner;

    public void OnConnectedToServer(NetworkRunner runner)
    {
        SpawnLocalPlayer(runner);

        if (runner.IsSharedModeMasterClient) 
        {
            Debug.Log("Spawning Magic Cubes!");
            SpawnMagicCubes(runner);
        }
    }

    private void SpawnMagicCubes(NetworkRunner runner)
    {
        foreach (CubeSpawnPoint cubeSpawnPoint in FindObjectsOfType<CubeSpawnPoint>()) 
        { 
          var cube =  runner.Spawn(magicCubePrefab,cubeSpawnPoint.gameObject.transform.position,cubeSpawnPoint.gameObject.transform.rotation);
          cube.transform.position = cubeSpawnPoint.transform.position;
        }
    }

    private void SpawnLocalPlayer(NetworkRunner runner)
    {
        NetworkObject player = runner.Spawn(playerPrefab,transform.position,transform.rotation);
        player.transform.position = new Vector3(4, 2, 4);
        followCamera.Follow = player.gameObject.GetComponentInChildren<PlayerCameraRoot>().gameObject.transform;
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        networkRunner = GetComponent<NetworkRunner>();
        StartGameArgs startGameArgs = new StartGameArgs();
        startGameArgs.GameMode = GameMode.Shared;
        startGameArgs.SessionName = "FormaticaProva";
        startGameArgs.PlayerCount = 10;

        networkRunner.StartGame(startGameArgs);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
