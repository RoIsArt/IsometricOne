using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ServiceLoader _serviceLoader;

    private void Awake()
    {
        _serviceLoader.RegisterServices();
        _serviceLoader.InitializeServices();
        _serviceLoader.AddDisposables();
    }
}
