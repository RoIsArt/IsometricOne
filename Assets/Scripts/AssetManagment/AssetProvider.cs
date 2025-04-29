using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.AssetManagment
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}
