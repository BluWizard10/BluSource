#if UDONSHARP
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon
{
    [RequireComponent(typeof(Collider)), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerColliderExitOnly : UdonSharpBehaviour
    {
        [Header("On Exit Collider")]
        [Tooltip("Assign an Udon Behaviour that should run when a Player leaves this Collider's boundaries.")]
        public UdonBehaviour exitReceiver;

        [Tooltip("Change this if the target Script uses a different Custom Event name.")]
        public string customExitName = "_BluEvent";

        public override void OnPlayerTriggerExit(VRCPlayerApi playerApi)
        {
            if (Networking.LocalPlayer != null)
            {
                if (playerApi.isLocal)
                {
                    exitReceiver.SendCustomEvent(customExitName);
                }
            }
        }
    }
}
#endif