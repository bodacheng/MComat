using System;
using UniRx;
using PlayFab.ClientModels;

public class MailItemInstance : ItemInstance
{
    readonly Subject<bool> _read = new();
    public IObservable<bool> ReadObservable => _read;
    
    public void Set()
    {
        _read.OnNext(!NotClaimed());
    }

    public bool NotClaimed()
    {
        return RemainingUses.HasValue && RemainingUses.Value > 0;
    }
}
