namespace AssetManagment
{
    public class AssetPath
    {
        public const string HUDPath = "UI/HUD";
        public const string GridPath = "Prefabs/Grid/CellsGrid";
        
        private static readonly string SceneBootstrapperPath = "SceneBootstrappers";

        public static string GetSceneBootstrapperPath(string sceneName)
        {
            return SceneBootstrapperPath + $"/{sceneName}Bootstrapper";
        }
    }
}
