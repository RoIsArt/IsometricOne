using Cells;
using UnityEngine;

public class Miner : IMiner
{
    private readonly Cooldown _mineCooldown = new(1);
    private Cell[,] _cells;
    private int _totalCount;
    private int _minePerSecond;

    public void Initialize(Cell[,] cells)
    {
        _cells = cells;
    }
    
    public void Mine()
    {
        if (_mineCooldown.IsReady)
        {
            _totalCount += _minePerSecond;
            _mineCooldown.Reset();
        }
    }

    public void Refresh()
    {
        _minePerSecond = 0;
        
        foreach (Cell cell in _cells)
        {
            if (cell)
            {
                _minePerSecond += cell.MinePerSecond;
            }
        }
    }
}