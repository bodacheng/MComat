using PlayFab.ClientModels;
using System;

public partial class CloudScript
{
    public static void ArcadeProgress(string _stage, Action<ExecuteCloudScriptResult> claimQuestRewardSuccess)
    {
        // 之所以把更新关卡进度和获取报酬分开处理，是因为当时把这些处理写到一个cloud函数里的时候，
        // 竟然有一定概率playfab不给执行关卡进度更新所触发的角色获取rule，于是我们才决定在这个部分不要把各种处理集中在一个瞬间
        // 原本在cloudscript内会查询是不是第一次通某关，给去掉了也是这个原因。
        // 所以，如果玩家在更新了关卡的瞬间掉线，导致随后的获取报酬云函数没执行，完全可能获得不了这一关的报酬。但关卡进度更新了的话应该是能拿到对应的角色
        ExecuteCloudScriptMainSceneCommon(
            new ExecuteCloudScriptRequest()
            {
                FunctionName = "completedLevel",
                FunctionParameter = new { stage = _stage },
            },
            (x) =>
            {
                ExecuteCloudScriptMainSceneCommon(
                    new ExecuteCloudScriptRequest
                    {
                        FunctionName = "claimQuestReward",
                        FunctionParameter = new { stage = _stage }
                    },
                    (x)=>
                    {
                        claimQuestRewardSuccess(x);
                        var stageInt = Int32.Parse(_stage);
                        switch (stageInt)
                        {
                            case 1:
                                PopupLayer.ArrangeWarnWindowUnitIcon(" tetsuya  " + Translate.Get("GotNewUnit"), "2");
                                break;
                            case 5:
                                PopupLayer.ArrangeWarnWindowUnitIcon(" adam  " + Translate.Get("GotNewUnit"), "1");
                                break;
                            case 20:
                                PopupLayer.ArrangeWarnWindowUnitIcon(" maggie  " + Translate.Get("GotNewUnit"), "4");
                                break;
                            case 35:
                                PopupLayer.ArrangeWarnWindowUnitIcon(" yuta  " + Translate.Get("GotNewUnit"), "7");
                                break;
                            case 50:
                                PopupLayer.ArrangeWarnWindowUnitIcon(" sybill  " + Translate.Get("GotNewUnit"), "6");
                                break;
                            case 100:
                                PopupLayer.ArrangeWarnWindowUnitIcon(" et  " + Translate.Get("GotNewUnit"), "5");
                                break;
                        }
                    }
                );
            }
        );
    }
}