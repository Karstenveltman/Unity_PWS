using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Http;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using Unity.Networking.Transport.Relay;
using TMPro;

public class RelayManager : MonoBehaviour
{
    private int maxPlayers = 1;   
    public MPbombspawner bombspawner;
    string joinCode;
    [SerializeField] TextMeshProUGUI joinCodeText;
    

    private void Start() {
        //gets the playernr, which is decided in the mainmenu
        int playernr = PlayerPrefs.GetInt("PlayerNr");
        if (playernr == 0) {
            CreateRelay();
            //tells the bombspawner that player 1 has joined, letting it wait for player 2
            bombspawner.networkStarted = true;
        }
        else if (playernr == 1) {
            joinCode = PlayerPrefs.GetString("joinCode");
            JoinRelay(joinCode);
        }
    }
    public async void CreateRelay() {
        //authentication isn't placed in a different function because the game freezes if it is
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in as" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        try {
            //connect to the relay server
            Allocation relayAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(relayAllocation.AllocationId);
            Debug.Log("Joincode: " + joinCode);
            
            //open up a connection
            RelayServerData serverData = new RelayServerData(relayAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);
            NetworkManager.Singleton.StartHost();

            //print the joincode on screen
            joinCodeText.text = joinCodeText.text + joinCode;

        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(string joinCode) {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in as" + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        try {
            Debug.Log("Using Joincode: " + joinCode);
            JoinAllocation joinRelayAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData serverData = new RelayServerData(joinRelayAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);
            NetworkManager.Singleton.StartClient();
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }
}
