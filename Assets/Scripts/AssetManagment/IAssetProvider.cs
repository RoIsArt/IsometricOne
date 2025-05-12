using Infrastructure.Services;
using UnityEngine;

namespace AssetManagment
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
    }
}