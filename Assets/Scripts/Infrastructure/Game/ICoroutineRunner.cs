using System.Collections;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Game
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(IEnumerator routine);
    }
}