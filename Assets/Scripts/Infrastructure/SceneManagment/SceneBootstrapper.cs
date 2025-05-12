using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.SceneManagment
{
    public abstract class SceneBootstrapper : MonoBehaviour
    {
        public abstract void ConstructScene(DiContainer container);
    }
}