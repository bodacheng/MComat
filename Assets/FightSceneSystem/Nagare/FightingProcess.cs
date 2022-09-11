using DummyLayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using mainMenu;
using System.Collections.Generic;
using Log;
using PlayFab;

namespace FightScene
{
    public class FightingProcess : FSceneProcess
    {
        FightingStepLayer _layer;
        
        public FightingProcess()
        {
            Step = SceneStep.Fighting;
            nextProcessStep = SceneStep.FightOver;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return FightLogger.value.GameOver.Value;
        }
        
        public override void ProcessEnter()
        {
            _layer = FightingStepLayer.Get();
            if (NetFightScene.Fight.EventType == FightEventType.Screensaver)
            {
                var TitleScreenLayer = UILayerLoader.Load(NetFightScene.target.T.gameObject, "TitleScreenLayer") as TitleScreenLayer;
                TitleScreenLayer.Initialise();
                PopupLayer.LightUp(1f);
            }
            else
            {
                var fightingStepLayer = FightingStepLayer.Get();
                fightingStepLayer.gameObject.SetActive(true);
            }
            
            RTFightManager.target.ModeStart();
        }
        
        public override void ProcessEnd()
        {
            if (NetFightScene.Fight.EventType == FightEventType.Screensaver)
            {
                UILayerLoader.Remove("TitleScreenLayer");
            }
            else
            {
                FightingStepLayer.Close();
            }
            
            var data_Centers = new List<Data_Center>();
            data_Centers.AddRange(RTFightManager.target.team1.TeamMembers.GetValues());
            data_Centers.AddRange(RTFightManager.target.team2.TeamMembers.GetValues());
            HitBoxLogTable.Instance.SkillLog(data_Centers);
            RTFightManager.target.disposables.Clear();
            RTFightManager.target.RefreshTimeDic.Clear();
            RTFightManager.target.ClearUnitData();
            FightLogger.value.WatchMissionsAbandon();
        }

        public override void LocalUpdate()
        {
            if (_layer != null)
            {
                RTFightManager.target.team1.localUpdate();
                RTFightManager.target.team2.localUpdate();
            }
        }
    }
}