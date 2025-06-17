using Cells;
using Infrastructure.Services;

public interface IMiner : IService
{
    void Mine();
    void Initialize(Cell[,] cells);
    void Refresh();
}