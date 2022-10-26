using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class ConnectionConfig : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private Button hostStopBtn;
    [SerializeField] private Button clientStopBtn;
    [SerializeField] private TextMeshProUGUI hostIP;

    private void Awake()
    {
        hostBtn.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = hostIP.text;
            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.StartClient();
        });
        
        hostStopBtn.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.Shutdown();
        });
        
        clientStopBtn.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.Shutdown();
        });
    }
}
