using System;
using UniRx;

public static class Currencies
{
    public static ReactiveProperty<int> DiamondCount { get; set; } = new();
    public static ReactiveProperty<int> CoinCount{ get; set; } = new();
    public static ReactiveProperty<int> ArenaTicket{ get; set; } = new();

    public static int SecondsToRecharge;
    public static int RechargeMax;
}
