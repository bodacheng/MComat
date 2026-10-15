using PlayFab.ClientModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using PlayFab;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;

public partial class CloudScript
{
    public static void EventBattleProgress(
        string stage,
        Action<ExecuteCloudScriptResult> claimQuestRewardSuccess,
        Action<PlayFab.PlayFabError> progressFail = null,
        Action<PlayFab.PlayFabError> rewardFailAfterProgress = null,
        bool showErrorPopup = true,
        CancellationToken cancellationToken = default,
        Func<bool> isCurrentResult = null)
    {
        if (cancellationToken.IsCancellationRequested || (isCurrentResult != null && !isCurrentResult()))
            return;

        // 之所以把更新关卡进度和获取报酬分开处理，是因为当时把这些处理写到一个cloud函数里的时候，
        // 竟然有一定概率playfab不给执行关卡进度更新所触发的角色获取rule，于是我们才决定在这个部分不要把各种处理集中在一个瞬间
        // 原本在cloudscript内会查询是不是第一次通某关，给去掉了也是这个原因。
        // 所以，如果玩家在更新了关卡的瞬间掉线，导致随后的获取报酬云函数没执行，完全可能获得不了这一关的报酬。但关卡进度更新了的话应该是能拿到对应的角色

        string levelKey;
        
        if (stage != null && stage.Contains("easy"))
        {
            levelKey = "easy";
        }
        else if (stage != null && stage.Contains("normal"))
        {
            levelKey = "normal";
        }
        else if (stage != null && stage.Contains("hard"))
        {
            levelKey = "hard";
        }
        else
        {
            var error = new PlayFab.PlayFabError
            {
                Error = PlayFab.PlayFabErrorCode.InvalidParams,
                ErrorMessage = "The event battle could not be identified. Please return to the menu and try again."
            };
            progressFail?.Invoke(error);
            if (showErrorPopup)
                PlayFabReadClient.ErrorReportStayInScene(error);
            return;
        }
        
        RunEventBattleProgressAsync(stage, levelKey, claimQuestRewardSuccess, progressFail,
            rewardFailAfterProgress, showErrorPopup, cancellationToken, isCurrentResult,
            TimeSpan.FromSeconds(30)).AsUniTask().Forget();
    }

    static async Task RunEventBattleProgressAsync(
        string stage, string levelKey, Action<ExecuteCloudScriptResult> success,
        Action<PlayFabError> progressFail, Action<PlayFabError> rewardFail,
        bool showErrorPopup, CancellationToken cancellationToken,
        Func<bool> isCurrentResult, TimeSpan timeout)
    {
        var ownerPlayFabId = PlayerAccountInfo.Me?.PlayFabId;
        bool IsCurrent()
        {
            return !cancellationToken.IsCancellationRequested &&
                !string.IsNullOrEmpty(ownerPlayFabId) &&
                PlayerAccountInfo.Me?.PlayFabId == ownerPlayFabId &&
                (isCurrentResult == null || isCurrentResult());
        }

        void Fail(PlayFabError error, bool progressCompleted)
        {
            if (!IsCurrent())
                return;

            (progressCompleted ? rewardFail ?? progressFail : progressFail)?.Invoke(error);
            if (showErrorPopup && IsCurrent())
                PlayFabReadClient.ErrorReportStayInScene(error);
        }

        if (!IsCurrent())
            return;

        // Both server mutations share one wall-clock budget. Do not use the generic
        // loading/retry helper: a claim may have granted currency before its reply is lost.
        using (var deadline = new CancellationTokenSource(timeout))
        using (var lifetime = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, deadline.Token))
        {
            bool progressCompleted = false;
            try
            {
                var progress = await ExecuteEventRequestAsync(new ExecuteCloudScriptRequest
                {
                    FunctionName = "checkAndUpdateEventBattleProgress",
                    FunctionParameter = new { eventBattleId = stage }
                }, lifetime.Token);
                await UniTask.SwitchToMainThread();
                if (!IsCurrent())
                    return;
                lifetime.Token.ThrowIfCancellationRequested();
                if (progress.error != null)
                {
                    Fail(progress.error, false);
                    return;
                }

                progressCompleted = true;
                var reward = await ExecuteEventRequestAsync(new ExecuteCloudScriptRequest
                {
                    FunctionName = "claimEventReward",
                    FunctionParameter = new { level = levelKey }
                }, lifetime.Token);
                await UniTask.SwitchToMainThread();
                if (!IsCurrent())
                    return;
                lifetime.Token.ThrowIfCancellationRequested();
                if (reward.error != null)
                {
                    Fail(reward.error, true);
                    return;
                }

                success?.Invoke(reward.result);
            }
            catch (OperationCanceledException)
            {
                await UniTask.SwitchToMainThread();
                if (deadline.IsCancellationRequested)
                    Fail(UnconfirmedEventResult(), progressCompleted);
            }
        }
    }

    static async Task<(ExecuteCloudScriptResult result, PlayFabError error)> ExecuteEventRequestAsync(
        ExecuteCloudScriptRequest request, CancellationToken cancellationToken)
    {
        var completion = new TaskCompletionSource<(ExecuteCloudScriptResult, PlayFabError)>(
            TaskCreationOptions.RunContinuationsAsynchronously);
        using (cancellationToken.Register(() => completion.TrySetCanceled(cancellationToken)))
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                // SDK callbacks only complete this gate. After timeout/destruction they
                // cannot start another request, close another layer, or credit a reward.
                PlayFabClientAPI.ExecuteCloudScript(request,
                    result => completion.TrySetResult((result, result == null ? UnconfirmedEventResult() :
                        result.Error == null ? null : ToPlayFabError(result))),
                    error => completion.TrySetResult((null, IsUnconfirmedEventRequest(error)
                        ? UnconfirmedEventResult() : error)));
            }
            catch (Exception)
            {
                // The SDK can throw synchronously, for example after logout.
                completion.TrySetResult((null, UnconfirmedEventResult()));
            }

            return await completion.Task;
        }
    }

    static bool IsUnconfirmedEventRequest(PlayFabError error)
    {
        return error == null || error.Error == PlayFabErrorCode.ConnectionError ||
            (error.HttpCode == 0 && (error.Error == PlayFabErrorCode.Unknown ||
                error.Error == PlayFabErrorCode.UnknownError)) ||
            error.HttpCode == 408 || error.HttpCode >= 500;
    }

    static PlayFabError UnconfirmedEventResult()
    {
        return new PlayFabError
        {
            Error = PlayFabErrorCode.UnknownError,
            ErrorMessage = "The event progress and reward state are unconfirmed. Please return to the menu and refresh your account data. The reward request will not be retried automatically."
        };
    }
}
