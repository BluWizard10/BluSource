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
    public class ToggleGameObjectList : UdonSharpBehaviour
    {
        [Tooltip("What GameObjects should be Toggled when this script runs?")]
        public GameObject[] targetObjects;

        public void _BluEvent()
        {
            foreach (GameObject targetObject in targetObjects)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }
        }
    }
}
#endif