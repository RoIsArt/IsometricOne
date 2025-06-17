using Cells;
using DatasAndConfigs;
using GameEvents;

namespace GamePlayServices
{
    public interface IHighlighter
    {
        void Highlight();
        void StartBuild();
        void EndBuild();
    }
}