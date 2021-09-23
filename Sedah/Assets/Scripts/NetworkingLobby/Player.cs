using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using SedahNetworking;
using Sedah;
using UnityEngine.AI;

[RequireComponent(typeof(NetworkMatch))]
public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    [SyncVar] public int playerIndex;
    [SerializeField] GameObject playerSpawn;
    NetworkMatch networkMatch;

    [SyncVar] public Match currentMatch;

    [SerializeField] GameObject playerLobbyUI;

    void Awake()
    {
        networkMatch = GetComponent<NetworkMatch>();
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            DontDestroyOnLoad(this.gameObject);
            localPlayer = this;
        }
        else
        {
            Debug.Log($"Spawning other player UI Prefab");
            playerLobbyUI = UILobby.instance.SpawnPlayerUIPrefab(this);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnStopClient()
    {
        Debug.Log($"Client Stopped");
        ClientDisconnect();
    }

    public override void OnStopServer()
    {
        Debug.Log($"Client Stopped on Server");
        ServerDisconnect();
    }

    /* 
        HOST MATCH
    */

    public void HostGame(bool publicMatch)
    {
        string matchID = MatchMaker.GetRandomMatchID();
        CmdHostGame(matchID, publicMatch);
    }

    [Command]
    void CmdHostGame(string _matchID, bool publicMatch)
    {
        matchID = _matchID;
        if (MatchMaker.instance.HostGame(_matchID, gameObject, publicMatch, out playerIndex))
        {
            Debug.Log($"<color=green>Game hosted successfully</color>");
            networkMatch.matchId = _matchID.ToGuid();
            TargetHostGame(true, _matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color=red>Game hosted failed</color>");
            TargetHostGame(false, _matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _matchID, int _playerIndex)
    {
        playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.HostSuccess(success, _matchID);
    }

    /* 
        JOIN MATCH
    */

    public void JoinGame(string _inputID)
    {
        CmdJoinGame(_inputID);
    }

    [Command]
    void CmdJoinGame(string _matchID)
    {
        matchID = _matchID;
        if (MatchMaker.instance.JoinGame(_matchID, gameObject, out playerIndex))
        {
            Debug.Log($"<color=green>Game Joined successfully</color>");
            networkMatch.matchId = _matchID.ToGuid();
            TargetJoinGame(true, _matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color=red>Game Joined failed</color>");
            TargetJoinGame(false, _matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _matchID, int _playerIndex)
    {
        playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        UILobby.instance.JoinSuccess(success, _matchID);
    }

    /* 
        DISCONNECT
    */

    public void DisconnectGame()
    {
        CmdDisconnectGame();
    }

    [Command]
    void CmdDisconnectGame()
    {
        ServerDisconnect();
    }

    void ServerDisconnect()
    {
        MatchMaker.instance.PlayerDisconnected(this, matchID);
        RpcDisconnectGame();
        networkMatch.matchId = string.Empty.ToGuid();
    }

    [ClientRpc]
    void RpcDisconnectGame()
    {
        ClientDisconnect();
    }

    void ClientDisconnect()
    {
        if (playerLobbyUI != null)
        {
            Destroy(playerLobbyUI);
        }
    }

    /* 
        SEARCH MATCH
    */

    public void SearchGame()
    {
        CmdSearchGame();
    }

    [Command]
    void CmdSearchGame()
    {
        if (MatchMaker.instance.SearchGame(gameObject, out playerIndex, out matchID))
        {
            Debug.Log($"<color=green>Game Found Successfully</color>");
            networkMatch.matchId = matchID.ToGuid();
            TargetSearchGame(true, matchID, playerIndex);
        }
        else
        {
            Debug.Log($"<color=red>Game Search Failed</color>");
            TargetSearchGame(false, matchID, playerIndex);
        }
    }

    [TargetRpc]
    void TargetSearchGame(bool success, string _matchID, int _playerIndex)
    {
        playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log($"MatchID: {matchID} == {_matchID} | {success}");
        UILobby.instance.SearchGameSuccess(success, _matchID);
    }

    /* 
        BEGIN MATCH
    */

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    void CmdBeginGame()
    {
        MatchMaker.instance.BeginGame(matchID);
        Debug.Log($"<color=red>Game Beginning</color>");
    }

    public void StartGame()
    { //Server

        TargetBeginGame();
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"MatchID: {matchID} | Beginning");
        //SceneManager.LoadScene(2, LoadSceneMode.Additive);
        //Additively load game scene
        //Scene scene = SceneManager.GetSceneByName("BenScene");
        //StartCoroutine(WaitLoadClient(scene));
        //CmdMoveObject();
        CmdSpawn();
    }

    [Command]
    void CmdMoveObject(NetworkConnectionToClient conn = null)
    {

    }

    IEnumerator WaitLoadServer(Scene scene)
    {
        yield return scene.isLoaded;
        //SceneManager.MoveGameObjectToScene(this.gameObject, scene);
        //GameObject.Find("Main Camera").SetActive(false);
        SceneManager.SetActiveScene(scene);


    }

    [Command]
    void CmdSpawn(NetworkConnectionToClient conn = null)
    {
        Debug.Log("Spawn");
        GameObject spawn = Instantiate(playerSpawn);
        //GameObject spawn2 = Instantiate(this.gameObject);
        NetworkServer.Spawn(spawn, conn);
    }
    IEnumerator WaitLoadClient(Scene scene)
    {
        yield return scene.isLoaded;
        GameObject.Find("Main Camera").SetActive(false);
        SceneManager.SetActiveScene(scene);
        CmdSpawn();
        //Instantiate(this.gameObject);
        //this.GetComponent<EntityStateMachine>().enabled = true;
        //this.GetComponent<PlayerMovement>().enabled = true;
        //this.GetComponent<Health>().enabled = true;
        //this.GetComponent<CharacterObject>().enabled = true;
        //this.GetComponent<Player>().enabled = true;
        //this.GetComponent<PlayerCamera>().enabled = true;
        //this.GetComponent<PlayerAutoAttack>().enabled = true;
        //this.GetComponent<PlayerAbility>().enabled = true;
        //this.GetComponent<NavMeshAgent>().enabled = true;
        //this.GetComponent<MeshRenderer>().enabled = true;
    }

}