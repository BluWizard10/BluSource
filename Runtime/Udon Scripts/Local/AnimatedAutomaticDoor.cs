#if UDONSHARP
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon
{
    [RequireComponent(typeof(Collider)), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AnimatedAutomaticDoor : UdonSharpBehaviour
    {
        public Animator animator;
        public string enterBool = "_ProximityTrigger";
        public string exitBool = "_ProximityTrigger";
        private float exitTime = 3.0f;
        private bool exitSequence = false;
    
        private void Update()
        {
            if (exitSequence == true)
            {
                exitTime -= Time.deltaTime;
                if (exitTime <= 0.0f)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.Owner, "TriggerAnim");
                    exitSequence = false;
                    exitTime = 3.0f;
                }
            }
        }
    
        public void TriggerAnim()
        {
            animator.SetBool(exitBool, true);
        }
    
        public override void OnPlayerTriggerEnter(VRCPlayerApi playerApi)
        {
            if (Networking.LocalPlayer != null)
            {
                if (playerApi.isLocal)
                {
                    animator.SetBool(enterBool, true);
                }
            }
        }
    
        public override void OnPlayerTriggerExit(VRCPlayerApi playerApi)
        {
            if (Networking.LocalPlayer != null)
            {
                if (playerApi.isLocal)
                {
                    animator.SetBool(exitBool, false);
                    exitSequence = true;
                }
            }
        }
    }
}
#endif