using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cells
{
    [Serializable]
    public class ConnectingSide
    {
        [FormerlySerializedAs("_side")] [SerializeField] private SideName sideName;
        [FormerlySerializedAs("_isConnected")] [SerializeField] private bool isConnected;

        public SideName SideName => sideName;
        public bool IsConnected => isConnected;
        
        public void Connect() => isConnected = true;
        public void Disconnect() => isConnected = false;
    }
}