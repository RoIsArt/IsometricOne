using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectingCell : Cell
{
    protected List<ConnectingSide> _connectingSides;

    public void Construct(ConnectingCellData data, Vector2Int index)
    {
        base.Construct(data, index);
        _connectingSides = new List<ConnectingSide>();
        _connectingSides = data.ConnectingSides;
    }

    public List<ConnectingSide> ConnectingSides { get { return _connectingSides; } }

    public ConnectingSide GetConnectingSide(Side fromSide)
    {
        return _connectingSides.FirstOrDefault(x => x.Side == fromSide);
    }

    public bool ContainSide(Side side)
    {
        foreach (var connectingSide in _connectingSides)
        {
            if (connectingSide.Side == side) return true;
        }

        return false;
    }
}
