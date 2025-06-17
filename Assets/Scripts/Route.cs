using System.Collections.Generic;
using System.Linq;
using Cells;
using Properties;

public class Route
{
    private List<Cell> _cells;
    private Property<int> _miningPerSecond;
    private bool _isReady;

    public Route(Cell firstCell)
    {
        _cells = new();
        _miningPerSecond = new Property<int>(0);
        _isReady = false;

        _cells.Add(firstCell);
    }

    public List<Cell> Cells {  get { return _cells; } }
    public Property<int> MiningPerSecond { get { return _miningPerSecond; } }
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
