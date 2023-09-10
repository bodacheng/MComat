using System;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class PlayFabReadClient
{
    private static IDictionary<string, Award> _stageAward;
    public static IDictionary<string, Award> StageAwards => _stageAward;
    private static IDictionary<string, Award> _gangbangAward;
    public static IDictionary<string, Award> GangbangAwards => _gangbangAward;
    
    public static void GetStageRewardInfo(Action<bool> finished)
    {
        PlayFabClientAPI.GetTitleData(
            new GetTitleDataRequest
            {
                Keys = new List<string>() {"stage_awards", "gangbang_awards"}
            }, 
            result =>
            {
                var stageAwardObject = result.Data["stage_awards"];
                var stageAwards = JsonConvert.DeserializeObject<List<StageAward>>(stageAwardObject);
                _stageAward = new Dictionary<string, Award>();
                foreach (var kv in stageAwards)
                {
                    if (!_stageAward.ContainsKey(kv.stageKey))
                    {
                        _stageAward.Add(kv.stageKey, kv.award);
                    }
                }
                
                var gangbangAwardObject = result.Data["gangbang_awards"];
                var gangbangAwards = JsonConvert.DeserializeObject<List<StageAward>>(gangbangAwardObject);
                _gangbangAward = new Dictionary<string, Award>();
                foreach (var kv in gangbangAwards)
                {
                    if (!_gangbangAward.ContainsKey(kv.stageKey))
                    {
                        _gangbangAward.Add(kv.stageKey, kv.award);
                    }
                }
                
                finished.Invoke(true);
            },
            (x) =>
            {
                finished(false);
                ErrorReport(x);
            }
        );
    }
}
