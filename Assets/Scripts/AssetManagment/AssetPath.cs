namespace AssetManagment
{
    public class AssetPath
    {
        public const string HUDPath = "UI/HUD";
        public const string GridPath = "Prefabs/Grid/CellsGrid";
        public const string BackgroundPath = "BackGround/BackGround";
        
        private static readonly string SceneBootstrapperPath = "SceneBootstrappers";

        public static string GetSceneBootstrapperPath(string sceneName) => 
            $"{SceneBootstrapperPath}/{sceneName}Bootstrapper";
    }
}
