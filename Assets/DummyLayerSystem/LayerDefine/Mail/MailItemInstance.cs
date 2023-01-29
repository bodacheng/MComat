using System;
using UniRx;
using PlayFab.ClientModels;

public class MailItemInstance : ItemInstance
{
    readonly Subject<bool> read = new();
    public IObservable<bool> ReadObservable => read;
    
    public void Set()
    {
        read.OnNext(!NotRead());
    }

    public bool NotRead()
    {
        return (RemainingUses.HasValue && RemainingUses.Value > 0);
    }
}
