using System.Collections.Generic;
using System.Linq;
using Cells;
using Properties;

public class Route
{
    private readonly List<Cell> _cells;
    private bool _isReady;

    public Route(Cell firstCell)
    {
        _cells = new();
        _isReady = false;

        _cells.Add(firstCell);
    }

    public List<Cell> Cells {  get { return _cells; } }
    public Cell First {  get { return _cells.First(); } }
    public Cell Last {  get { return _cells.Last(); } }
    public bool IsReady { get { return _isReady; } }

    public void Add(Cell cell)
    {
        _cells.Add(cell);
    }

    public void Remove(Cell cell)
    {
        if(_cells.Contains(cell))
        {
            for (var i = _cells.IndexOf(cell); i < _cells.Count; i++)
            {
                _cells.RemoveAt(i);
            }

            _isReady = false;
        }
    }
}
