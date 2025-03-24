using System.Collections;
using UnityEngine;

public class Miner
{
    private Route _route;
    private Wallet _wallet;

    public Miner(CellsGrid cellsGrid)
    {
        var sourceCell = cellsGrid[cellsGrid.SourcePosition];
        _route = new Route(sourceCell); 
        _wallet = new Wallet();
    }
    
    public IEnumerator Mine()
    {
        _wallet.Total.Value += _route.MiningPerSecond.Value;
        yield return new WaitForSeconds(1);
    }
}
