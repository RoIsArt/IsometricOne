using Assets.Scripts.AssetManagment;
using Assets.Scripts.Infrastructure.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Infrastructure
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;

        public UIFactory(IAssets assets) 
        {
            _assets = assets;
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPath.HUD_PATH);
        }
    }
}
