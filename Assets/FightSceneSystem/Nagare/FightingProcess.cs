using DummyLayerSystem;
using System.Collections.Generic;
using Log;

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
            _layer = UILayerLoader.Get<FightingStepLayer>();
            if (FightScene.Fight.EventType == FightEventType.Screensaver)
            {
                var titleScreenLayer = UILayerLoader.Load<TitleScreenLayer>();
                titleScreenLayer.Initialise();
                HighLightLayer.LightUp(1f);
            }
            else
            {
                _layer.gameObject.SetActive(true);
            }
            if (FightScene.Fight.RunTutorial)
                _layer.OpenTutorial();
            RTFightManager.Target.ModeStart();
        }
        
        public override void ProcessEnd()
        {
            if (FightScene.Fight.EventType == FightEventType.Screensaver)
            {
                UILayerLoader.Remove<TitleScreenLayer>();
            }
            else
            {
                FightingStepLayer.Close();
            }
            
            var dataCenters = new List<Data_Center>();
            dataCenters.AddRange(RTFightManager.Target.team1.teamMembers.GetValues());
            dataCenters.AddRange(RTFightManager.Target.team2.teamMembers.GetValues());
            HitBoxLogTable.Instance.SkillLog(dataCenters);
            RTFightManager.Target.Disposables.Clear();
            RTFightManager.Target.RefreshTimeDic.Clear();
            RTFightManager.Target.ClearUnitData();
            FightLogger.value.WatchMissionsAbandon();
        }

        public override void LocalUpdate()
        {
            if (_layer != null)
            {
                RTFightManager.Target.team1.LocalUpdate();
                RTFightManager.Target.team2.LocalUpdate();
            }
        }
    }
}