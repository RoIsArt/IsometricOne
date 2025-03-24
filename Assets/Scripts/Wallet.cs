using UnityEngine;

public class Wallet
{
    private Property<int> _total;

    public Wallet()
    {
        _total = new Property<int>(0);
    }

    public Property<int> Total { get { return _total; } }
}
