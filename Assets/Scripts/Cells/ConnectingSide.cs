using System;
using UnityEngine;

[Serializable]
public class ConnectingSide
{
    [SerializeField] private Side _side;
    [SerializeField] private bool _isConnected;

    public Side Side {  get { return _side; } }
    public bool IsConnected { get { return _isConnected; } }

    public void Connected()
    {
        _isConnected = true;
    }

    public void Disconnected()
    {
        _isConnected = false;
    }
}