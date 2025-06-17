using UnityEngine;

public class Cooldown
{
    private float _duration;
    private float _readyTime;

    public Cooldown(float duration)
    {
        _duration = duration;
        Reset();
    }

    public bool IsReady => 
        Time.time > _readyTime;

    public void Reset() => 
        _readyTime = Time.time + _duration;
}