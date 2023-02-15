/*
This script is unused, keeping it here in case it will be used at some point


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Http;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using Unity.Networking.Transport.Relay;


public class lobbyScript : MonoBehaviour {

    private Lobby hostLobby;
    private Lobby joinedLobby;
    private float heartbeatTimer;
    private float lobbyPollTimer;
    public const string KEY_START_GAME = "0";
    public relayScript relay;

    private async void Start() {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in as" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private async void Update() {
        //keeps the lobby alive so people can still find it after 30 seconds
        if (hostLobby != null) {
            heartbeatTimer -= Time.deltaTime;
            if (heartbeatTimer < 0f) {
                float heartbeatTimerMax = 15;
                heartbeatTimer = heartbeatTimerMax;

                await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
        HandleLobbyPolling();
    }
    private async void HandleLobbyPolling() {
        if (joinedLobby != null) {
            lobbyPollTimer -= Time.deltaTime;
            if (lobbyPollTimer < 0f) {
                float lobbyPollTimerMax = 1.1f;
                lobbyPollTimer = lobbyPollTimerMax;

                joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                if (joinedLobby.Data[KEY_START_GAME].Value != "0") {
                    //Start game
                    if (!IsLobbyHost()) {
                        relay.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value);
                    }
                    joinedLobby = null;

                    //OnGameStarted?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public bool IsLobbyHost() {
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }

    async void CreateLobby() {
        try {
            string LobbyName = "MyLobby";
            int maxPlayers = 2;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions {
                IsPrivate = true,
                Data = new Dictionary<string, DataObject> {
                    { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, "0")}
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(LobbyName, maxPlayers, createLobbyOptions);

            hostLobby = lobby;
            joinedLobby = hostLobby;

            Debug.Log("Created Lobby! " + lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Id + " " + lobby.LobbyCode);
        }
        catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    async void ListLobbies() {
        try {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions {
                Count = 25,
                Filters = new List<QueryFilter> {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder> {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results) {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    async void JoinLobby(string lobbyCode) {
        try {
            await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);

            Debug.Log("Joined lobby with code " + lobbyCode);
        }
        catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    async void QuickJoinLobby() {
        try {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    } 
    async void LeaveLobby() {
        try {
            await LobbyService.Instance.RemovePlayerAsync(hostLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }
    async void KickPlayer() {
        try {
            await LobbyService.Instance.RemovePlayerAsync(hostLobby.Id, hostLobby.Players[1].Id);
        }
        catch (LobbyServiceException e) {
            Debug.Log(e);
        }
    }

    public async void StartGame() {
        if (IsLobbyHost()) {
            try {
                string relayCode = await relay.CreateRelay();

                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(hostLobby.Id, new UpdateLobbyOptions {
                    Data = new Dictionary<string, DataObject> {
                        { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, relayCode)}
                    }
                });
                joinedLobby = lobby;
            }
            catch (LobbyServiceException e) {
                Debug.Log(e);
            }
        }
    } 
}
*/