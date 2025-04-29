using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.AssetManagment
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
    }
}