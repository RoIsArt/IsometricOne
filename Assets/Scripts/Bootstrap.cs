using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GroundStateMachine _groundStateMachine;

    private void Awake()
    {
        _groundStateMachine.Init();
    }
}
