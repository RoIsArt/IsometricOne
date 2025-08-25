using System.Collections.Generic;
using Cells;
using Infrastructure.Services;

public interface IRouteFactory : IService
{
    void CreateRoute(List<Cell> cells);
}