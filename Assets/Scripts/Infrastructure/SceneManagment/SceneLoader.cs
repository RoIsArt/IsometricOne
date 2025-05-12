using System;
using System.Collections;
using Infrastructure.Game;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagment
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        [Inject]
        public SceneLoader(ICoroutineRunner runner) =>
            _coroutineRunner = runner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartRoutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if(SceneManager.GetActiveScene().name == nextScene) 
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
