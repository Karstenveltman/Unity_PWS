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


public class relayScript : MonoBehaviour
{
    private int maxPlayers = 1;
    private async void Start() {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async void CreateRelay() {
        try {
            //connect to the relay server
            Allocation relayAllocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(relayAllocation.AllocationId);
            
            //open up a connection
            RelayServerData serverData = new RelayServerData(relayAllocation, "dlts");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);
            NetworkManager.Singleton.StartHost();

        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    private async void JoinRelay(string joinCode) {
        try {
            JoinAllocation joinRelayAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData serverData = new RelayServerData(joinRelayAllocation, "dlts");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }
}
