using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public partial class CloudScript
{
    public static void RequestAdReward()
    {
        ProgressLayer.Loading(string.Empty);
        PlayFabClientAPI.ExecuteCloudScript(
            new ExecuteCloudScriptRequest
            {
                FunctionName = "advertisementReward"
            },
            (x)=>
            {
                ProgressLayer.Close();
                var returnValue = (PlayFab.Json.JsonObject) x.FunctionResult;
                returnValue.TryGetValue("result", out var result);
                var resultJson = (PlayFab.Json.JsonObject) result;
                resultJson.TryGetValue("Balance", out var Balance);
                int.TryParse(Balance.ToString(), out var intBalance);
                resultJson.TryGetValue("BalanceChange", out var BalanceChange);
                int.TryParse(BalanceChange.ToString(), out var intBalanceChange);
                
                resultJson.TryGetValue("VirtualCurrency", out var VirtualCurrency);
                switch (VirtualCurrency.ToString())
                {
                    case "DM":
                        Currencies.DiamondCount.Value = intBalance;
                        break;
                    case "GD":
                        Currencies.CoinCount.Value = intBalance;
                        break;
                }
                PopupLayer.ArrangeWarnWindow("YOU GOT "+ intBalanceChange+ " " + VirtualCurrency.ToString());
            },
            (x)=>
            {
                Debug.Log(x.Error);
                ProgressLayer.Close();
                PopupLayer.ArrangeWarnWindow(x.ErrorMessage);
            }
        );
    }
}
