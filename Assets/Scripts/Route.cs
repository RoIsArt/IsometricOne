using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route
{
    private readonly List<Cell> _cells;
    private Property<int> _miningPerSecond;
    private bool _isReady;

    public Route(Cell firstCell)
    {
        _cells = new();
        _miningPerSecond = new Property<int>(0);
        _isReady = false;

        _cells.Add(firstCell);
    }

    public Property<int> MiningPerSecond { get { return _miningPerSecond; } }

    public void Add(Cell cell)
    {
        _cells.Add(cell);
        CheckRoute();
        //_miningPerSecond.Value += cell.Data.MiningPerSecond;
    }
    public void Remove(Cell cell)
    {
        if(_cells.Contains(cell))
        {
            var count = _cells.Count - _cells.IndexOf(cell);
            _cells.RemoveRange(_cells.IndexOf(cell), count);
        }
    }
    private void CheckRoute()
    {
        if(_cells.First() == _cells.Last())
        {
            _isReady = true;
        }
    }
}
