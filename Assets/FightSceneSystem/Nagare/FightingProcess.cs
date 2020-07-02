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
            FightScene.PreparingCanvas.gameObject.SetActive(false);
            FightScene.FightCanvas.gameObject.SetActive(true);
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
            if (Input.GetKey(KeyCode.Escape))
            {
                FightScene.PauseScene();
            }
            RealTimeGameProcessManager.target.FightTeam1.LocalFightingUpdate();
            RealTimeGameProcessManager.target.FightTeam2.LocalFightingUpdate();
        }
    }
}