using System.Collections.Generic;
using System.Linq;
using Cells;
using Properties;

public class Route
{
    private List<ConnectingCell> _cells;
    private Property<int> _miningPerSecond;
    private bool _isReady;

    public Route(ConnectingCell firstCell)
    {
        _cells = new();
        _miningPerSecond = new Property<int>(0);
        _isReady = false;

        _cells.Add(firstCell);
    }

    public List<ConnectingCell> Cells {  get { return _cells; } }
    public Property<int> MiningPerSecond { get { return _miningPerSecond; } }
    public ConnectingCell First {  get { return _cells.First(); } }
    public ConnectingCell Last {  get { return _cells.Last(); } }
    public bool IsReady { get { return _isReady; } }

    public void Add(ConnectingCell cell)
    {
        _cells.Add(cell);
    }

    public void Remove(ConnectingCell cell)
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
