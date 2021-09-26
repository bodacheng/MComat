using System;
using UniRx;
using PlayFab.ClientModels;

public class MailItemInstance : ItemInstance
{
    readonly Subject<bool> read = new Subject<bool>();
    public IObservable<bool> ReadObservable => read;
    
    public void Set()
    {
        read.OnNext(!(RemainingUses.HasValue && RemainingUses.Value > 0));
    }
}
