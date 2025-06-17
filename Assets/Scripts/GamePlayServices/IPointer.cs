using Cells;
using Infrastructure.Services;

namespace GamePlayServices
{
    public interface IPointer : IService
    {
        Cell GetPointedCell();
    }
}