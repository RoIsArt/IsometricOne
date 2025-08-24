using Cells;
using Infrastructure.Services;

namespace GamePlayServices
{
    public interface IMiner : IService
    {
        void Mine();
        void Initialize(Cell[,] cells);
        void AddRoute(Route route);
    }
}