
using UnityEngine;
using StarterAssets;
using Cinemachine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerNetwork : NetworkBehaviour
{

    [SerializeField] private Transform rootPlayerCamera;
    [SerializeField] private GameObject playerHud;
    [SerializeField] private CinemachineVirtualCamera cmVCamera;
    [SerializeField] private UICanvasControllerInput controllerInput;
    private MobileDisableAutoSwitchControls controllerSwitch;


    public override void OnNetworkSpawn()
    {
        if(IsOwner)
        {
            playerHud = FindObjectOfType<GameManager>().PlayerHud;
            playerHud.SetActive(true);

            controllerInput = playerHud.GetComponent<UICanvasControllerInput>();
            controllerInput.starterAssetsInputs = GetComponent<StarterAssetsInputs>();

            controllerSwitch = playerHud.GetComponent<MobileDisableAutoSwitchControls>();
            controllerSwitch.playerInput = GetComponent<PlayerInput>();

            cmVCamera = FindObjectOfType<CinemachineVirtualCamera>();
            cmVCamera.Follow = rootPlayerCamera;
        }

    }
    public void Update()
    {
        if(!IsOwner)
        {
            GetComponent<ThirdPersonController>().enabled = false;
        }
    }
}

