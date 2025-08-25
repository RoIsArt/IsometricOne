using System.Collections.Generic;
using System.Linq;
using Cells;
using Properties;

public class Route
{
    private List<Cell> _cells;

    public Route(List<Cell> cells)
    {
        _cells = cells;
    }

    public List<Cell> Cells => _cells;
}
