using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class PlayFabReadClient
{
    private static IDictionary<string, Award> stageAward;
    public static IDictionary<string, Award> StageAwards => stageAward;
    
    public static void GetStageRewardInfo(Action success)
    {
        PlayFabClientAPI.GetTitleData(
            new GetTitleDataRequest
            {
                Keys = new List<string>(){"stage_awards"}
            }, 
            result =>
            {
                var stageAwardObject = result.Data["stage_awards"];
                var stageAwards = JsonConvert.DeserializeObject<List<StageAward>>(stageAwardObject);
                stageAward = new Dictionary<string, Award>();
                foreach (var kv in stageAwards)
                {
                    if (!stageAward.ContainsKey(kv.stageKey))
                    {
                        stageAward.Add(kv.stageKey, kv.award);
                    }
                }
                success.Invoke();
            },
            ErrorReport);
    }
}
