using AssetManagment;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        [Inject]
        public UIFactory(IAssetProvider assetProvider) 
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHud()
        {
            return _assetProvider.Instantiate(AssetPath.HUDPath);
        }
    }
}
