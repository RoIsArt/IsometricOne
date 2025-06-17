using Cells;
using DatasAndConfigs;
using GameEvents;

namespace GamePlayServices
{
    public interface IHighlighter
    {
        void Highlight(OnCellMouseEnterEvent onCellMouseEnterEvent);
        void RemoveHighlight(OnCellMouseExitEvent onCellMouseExitEvent);
        void StartBuild();
        void EndBuild();
    }
}