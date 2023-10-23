#if UDONSHARP
using System;
using UdonSharp;
using UnityEngine;
using UnityEditor;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleGameObjectListSendCustomEvent : UdonSharpBehaviour
    {
        [Tooltip("What GameObjects should be Toggled when this script runs?")]
        public GameObject[] targetObjects;

        [Tooltip("Which UdonBehaviour do you want to have triggered at the same time?")]
        public UdonBehaviour TargetUdonBehaviour;

        [Tooltip("Change this if the target Script uses a different Custom Event name.")]
        public string customEventName;

        public void _BluEvent()
        {
            foreach (GameObject targetObject in targetObjects)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
            TargetUdonBehaviour.SendCustomEvent(customEventName);
        }
    }
}
#endif