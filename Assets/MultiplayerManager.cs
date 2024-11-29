using Cinemachine;
using Fusion;
using Fusion.Sockets;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour , INetworkRunnerCallbacks
{
    public NetworkObject playerPrefab;
    public NetworkObject roomManagerPrefab;

    public CinemachineVirtualCamera followCamera;
    public CinemachineVirtualCamera aimCamera;

    public NetworkRunner networkRunner;

    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI timeText;

    public void OnConnectedToServer(NetworkRunner runner)
    {
        SpawnLocalPlayer(runner);

        if (runner.IsSharedModeMasterClient) 
        {
            NetworkObject roomManager = runner.Spawn(roomManagerPrefab, transform.position, transform.rotation);
        }
    }



    private void SpawnLocalPlayer(NetworkRunner runner)
    {
        NetworkObject player = runner.Spawn(playerPrefab,transform.position,transform.rotation);
        followCamera.Follow = player.gameObject.GetComponentInChildren<PlayerCameraRoot>().gameObject.transform;
        aimCamera.Follow = player.gameObject.GetComponentInChildren<PlayerCameraRoot>().gameObject.transform;
        player.gameObject.GetComponent<ThirdPersonController>().aimCamera = aimCamera;
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
