using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Route
{
    private Stack<Cell> _cells;
    private Property<int> _miningPerSecond;

    public Route()
    {
        _cells = new Stack<Cell>();
        _miningPerSecond = new Property<int>();
    }

    public Property<int> MiningPerSecond { get { return _miningPerSecond; } }

    public void Add(Cell cell)
    {
        _cells.Push(cell);
        _miningPerSecond.Value += cell.Data.MiningPerSecond;
    }

    public void Remove(Cell cell)
    {
        if(Contain(cell))
        {
            var popCell = _cells.Pop();
            _miningPerSecond.Value -= popCell.Data.MiningPerSecond;

            while(popCell != cell) 
            {
                popCell = _cells.Pop();
                _miningPerSecond.Value -= popCell.Data.MiningPerSecond;
            }
        }
    }

    public bool Contain(Cell cell)
    {
        foreach (Cell item in _cells)
        {
            if (item == cell) return true;
        }

        return false;
    }
}
