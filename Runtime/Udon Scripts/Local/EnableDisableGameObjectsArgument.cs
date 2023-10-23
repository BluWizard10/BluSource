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
    public class EnableDisableGameObjectsArgument : UdonSharpBehaviour
    {
        [Header("GameObjects to Enable")]
        [Tooltip("List of GameObjects that should be Enabled when this script runs.")]
        public GameObject[] toEnable;
        
        [Header("GameObjects to Disable")]
        [Tooltip("List of GameObjects that should be Disabled when this script runs.")]
        public GameObject[] toDisable;

        public void _BluEvent()
        {
            foreach (GameObject enable in toEnable)
            {
                enable.SetActive(true);
            }

            foreach (GameObject disable in toDisable)
            {
                disable.SetActive(false);
            }
        }
    }  
}
#endif
