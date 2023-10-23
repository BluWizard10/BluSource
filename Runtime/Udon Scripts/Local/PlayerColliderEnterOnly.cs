#if UDONSHARP
using System;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon
{
    [RequireComponent(typeof(Collider)), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerColliderEnterOnly : UdonSharpBehaviour
    {
        [Header("On Enter Collider")]
        [Tooltip("Assign an Udon Behaviour that should run when a Player enters this Collider.")]
        public UdonBehaviour enterReceiver;

        [Tooltip("Change this if the target Script uses a different Custom Event name.")]
        public string customEnterName = "_BluEvent";
        
        public override void OnPlayerTriggerEnter(VRCPlayerApi playerApi)
        {
            if (Networking.LocalPlayer != null)
            {
                if (playerApi.isLocal)
                {
                    enterReceiver.SendCustomEvent(customEnterName);
                }
            }
        }
    }
}
#endif