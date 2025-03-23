using System.Collections;
using UnityEngine;

public class Miner
{
    private Route _route;
    private Wallet _wallet;
    public Miner()
    {
        _route = new Route(); 
        _wallet = new Wallet();
    }
    
    public IEnumerator Mine()
    {
        _wallet.Total.Value += _route.MiningPerSecond.Value;
        yield return new WaitForSeconds(1);
    }
}
