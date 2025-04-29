using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface IUIFactory : IService
    {
        void CreateHud();
    }
}