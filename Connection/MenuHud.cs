using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class MenuHud : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private Button hostStopBtn;
    [SerializeField] private Button clientStopBtn;
    [SerializeField] private TMP_InputField hostIP;
    [SerializeField] private TMP_InputField clientIP;
    [SerializeField] private TextMeshProUGUI gameVersion;

    private void Awake()
    {
        hostIP.text = clientIP.text = "192.168.254.169";

        hostBtn.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = hostIP.text;
            // NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = hostIP.text;
            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(()=>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = clientIP.text;
            // NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = clientIP.text;
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

        gameVersion.text = Application.version;
    }
}
