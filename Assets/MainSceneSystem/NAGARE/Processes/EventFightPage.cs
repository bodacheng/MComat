using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;
using PlayFab;
using PlayFab.ClientModels;

public class EventFightPage : MSceneProcess
{
    private EventBattleTop layer;
    private DateTime currentTime;
    public EventFightPage()
    {
        Step = MainSceneStep.EventFight;
    }
    
    void TimeLoadFinished(bool value)
    {
        missionWatcher.Finish("timeLoadFinished", value);
    }
    
    void ProgressLoadFinished(bool value)
    {
        missionWatcher.Finish("progressLoadFinished", value);
    }
    
    public override void ProcessEnter()
    {
        missionWatcher = new MissionWatcher(
            new List<string>()
            {
                "timeLoadFinished","progressLoadFinished"
            },
            ()=>
            {
                _ProcessEnter().Forget();
            },
            () =>
            {
                SetLoaded(true);
            }
        );
        
        PlayFabClientAPI.GetTime(new GetTimeRequest(), 
            result =>
            {
                currentTime = result.Time;
                TimeLoadFinished(true);
            }, 
            (x)=>
            {
                PlayFabReadClient.ErrorReport(x);
                TimeLoadFinished(false);
            });
        
        PlayFabReadClient.GetCompletedLevels(
            (x) =>
            {
                EventModeManager.Instance.OnCloudScriptSuccess(x);
                ProgressLoadFinished(true);
            },
            (e) =>
            {
                ProgressLoadFinished(false);
            }
        );
    }

    async UniTask _ProcessEnter()
    {
        await EventModeManager.Instance.InitializeRandomMode(currentTime.ToString("yyyyMMdd"));
        layer = UILayerLoader.Load<EventBattleTop>();
        layer.SetupCommon(EventModeManager.Instance.CompletedLevels, 
            EventModeManager.Instance.EasyMode, EventModeManager.Instance.NormalMode, EventModeManager.Instance.HardMode);
        var unit = EventModeManager.Instance.GetRepresentativeUnit();
        if (unit != null)
        {
            await layer.IconButtonFeature(unit);
        }
        SetLoaded(true);
    }
    
    public override void ProcessEnd()
    {
        UILayerLoader.Remove<EventBattleTop>();
    }
}
