using UnityEngine;

public class Wallet
{
    private Property<int> _total;

    public Wallet()
    {
        _total = new Property<int>();
    }

    public Property<int> Total { get { return _total; } }
}
