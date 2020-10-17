using UnityEngine;

namespace FightScene
{
    public class FightingProcess : FSceneProcess
    {
        public FightingProcess(NetFightScene _NetFightScene)
        {
            Step = SceneStep.Fighting;
            nextProcessStep = SceneStep.FightOver;
            EelementsInherit(_NetFightScene);
        }
        
        public override bool CanEnterOtherProcess()
        {
            return fightLogger.GameOver.Value;
        }
        
        public override void ProcessEnter()
        {
            if (FightSceneNote.nextBattle._fightEventType == FightEventType.Screensaver)
            {
                FightScene.ScreensaverCanvas.gameObject.SetActive(true);
                FightScene.FightCanvas.gameObject.SetActive(false);
            }else{
                FightScene.ScreensaverCanvas.gameObject.SetActive(false);
                FightScene.FightCanvas.gameObject.SetActive(true);
            }
            FightScene.PreparingCanvas.gameObject.SetActive(false);            
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
            FightScene.PressedStartButton();
        }
        
        public override void ProcessEnd()
        {
            fightLogger.WatchMissionsAbandon();
            FightScene.FightCanvas.gameObject.SetActive(false);
        }

        public override void LocalUpdate()
        {
            RealTimeGameProcessManager.target.FightTeam1.LocalFightingUpdate();
            RealTimeGameProcessManager.target.FightTeam2.LocalFightingUpdate();
        }
    }
}