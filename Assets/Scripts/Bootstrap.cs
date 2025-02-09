using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GroundStateMachine _ground;

    private void Awake()
    {
        _ground.Init();
    }
}
