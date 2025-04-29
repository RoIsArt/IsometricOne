using System.Collections;
using UnityEngine;

public class Miner
{
    private readonly Wallet _wallet;

    private Route _route;
    public Miner(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void SetRoute(Route route)
    {
        _route = route;
    }
    
    public IEnumerator Mine()
    {
        _wallet.Total.Value += _route.MiningPerSecond.Value;
        Debug.Log(_wallet.Total.Value);

        yield return new WaitForSeconds(1);
    }
}
