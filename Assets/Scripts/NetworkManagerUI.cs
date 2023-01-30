using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    public MPbombspawner bombspawner;

    private void Awake() {
        hostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            bombspawner.OnNetworkStart();
        });
        clientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            bombspawner.OnNetworkStart();
        });
    }
}
