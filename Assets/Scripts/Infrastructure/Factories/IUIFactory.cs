using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IUIFactory : IService
    {
        GameObject CreateHud();
    }
}