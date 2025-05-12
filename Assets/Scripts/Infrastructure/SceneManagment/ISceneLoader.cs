using System;
using Infrastructure.Services;

namespace Infrastructure.SceneManagment
{
    public interface ISceneLoader : IService
    {
        void Load(string name, Action onLoaded = null);
    }
}