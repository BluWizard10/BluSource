#if UDONSHARP
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EnableGameObjectsArgument : UdonSharpBehaviour
    {
        [Header("GameObjects to Enable")]
        [Tooltip("List of GameObjects that should be Enabled when this script runs.")]
        public GameObject[] toEnable;

        public void _BluEvent()
        {
            foreach (GameObject enable in toEnable)
            {
                enable.SetActive(true);
            }
        }
    }
}
#endif