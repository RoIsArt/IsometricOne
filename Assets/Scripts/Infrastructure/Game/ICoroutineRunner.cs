using System.Collections;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Game
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartRoutine(IEnumerator routine);
        void StopRoutine(Coroutine routine);
    }
}