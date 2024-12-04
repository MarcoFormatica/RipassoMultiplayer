using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomConfig
{
    public static string roomNameSelected;
    public static int roomTimeSelected;
}

public class LobbyController : MonoBehaviour , INetworkRunnerCallbacks
{
    NetworkRunner networkRunner;
    public GameObject roomButtonPrefab;
    public RectTransform contentRoomList;
    public Button createNewRoomButton;

    public TMP_InputField roomNameInputField; 
    public TMP_InputField timeInputField; 

    public void ClearRoomList()
    {
        foreach(Button bt in contentRoomList.GetComponentsInChildren<Button>())
        {
            DestroyImmediate(bt.gameObject);
        }
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log(nameof(OnSessionListUpdated) + " CALLED");
        ClearRoomList();
        foreach (SessionInfo session in sessionList) 
        {
            GameObject roomButtonGO = Instantiate(roomButtonPrefab, contentRoomList);
            roomButtonGO.GetComponentInChildren<TextMeshProUGUI>().text = session.Name + " ("+ session.PlayerCount+"/"+session.MaxPlayers+")";
            roomButtonGO.GetComponent<SessionNameHolder>().sessionName = session.Name;

        }
    }



    public void OnConnectedToServer(NetworkRunner runner)
    {

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

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    private void Awake()
    {
        networkRunner = GetComponent<NetworkRunner>();
        networkRunner.JoinSessionLobby(SessionLobby.Shared);
        ClearRoomList();
        createNewRoomButton.onClick.AddListener(OnCreateNewRoomButtonClickedCallback);

        roomNameInputField.text = System.Guid.NewGuid().ToString();
        timeInputField.text = "60";
    }

    public void OnCreateNewRoomButtonClickedCallback()
    {
        int selectedTime = int.Parse(timeInputField.text);
        string roomNameSelected = roomNameInputField.text;

        bool existsRoomWithTheSameName = new List<SessionNameHolder>(FindObjectsOfType<SessionNameHolder>()).Exists(x=>x.sessionName == roomNameSelected);

        if (selectedTime > 0 && roomNameSelected!="" && existsRoomWithTheSameName==false)
        {
            RoomConfig.roomNameSelected = roomNameSelected;
            RoomConfig.roomTimeSelected = selectedTime;
            SceneManager.LoadScene(1);
        }

    }
}
