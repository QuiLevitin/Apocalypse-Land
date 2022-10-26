using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FishNet.Transporting.Tugboat
{
    public class IPConfiguration : MonoBehaviour
    {
        // [SerializeField] private string inputIP = "localhost";
        [SerializeField] private TextMeshProUGUI inputIPField;
        void Start()
        {
        }


        public void SetIPConfig()
        {
            // _tugboat.SetClientAddress(inputIPField.text);
        }

    }
}


