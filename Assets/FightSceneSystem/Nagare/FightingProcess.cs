namespace FightScene
{
    public class FightingProcess : FSceneProcess
    {
        public FightingProcess()
        {
            Step = SceneStep.Fighting;
            nextProcessStep = SceneStep.FightOver;
        }
        
        public override bool CanEnterOtherProcess()
        {
            return FightLogger.target.GameOver.Value;
        }
        
        public override void ProcessEnter()
        {
            //CameraMode nowC = RealTimeGameProcessManager.target._CameraManager.CModeDic[C_Mode.OneVOne];
            //if (nowC is OneVOneMode)
            //{
            //    DOTween.To(() => ((OneVOneMode)nowC).xzMax, (x) => ((OneVOneMode)nowC).xzMax = x, 16, 3f);
            //}
            if (FightSceneNote.nextBattle._fightEventType == FightEventType.Screensaver)
            {
                NetFightScene.target.ScreensaverCanvas.gameObject.SetActive(true);
                NetFightScene.target.FightCanvas.gameObject.SetActive(false);
            }else{
                NetFightScene.target.ScreensaverCanvas.gameObject.SetActive(false);
                NetFightScene.target.FightCanvas.gameObject.SetActive(true);
            }
            NetFightScene.target.PreparingCanvas.gameObject.SetActive(false);            
            FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
            FightScenePauseSupport.target.ControlCanvas.gameObject.SetActive(true);
            NetFightScene.target.PressedStartButton();
        }
        
        public override void ProcessEnd()
        {
            FightLogger.target.WatchMissionsAbandon();
            NetFightScene.target.FightCanvas.gameObject.SetActive(false);
        }

        public override void LocalUpdate()
        {
            RealTimeGameProcessManager.target.FightTeam1.LocalFightingUpdate();
            RealTimeGameProcessManager.target.FightTeam2.LocalFightingUpdate();
        }
    }
}