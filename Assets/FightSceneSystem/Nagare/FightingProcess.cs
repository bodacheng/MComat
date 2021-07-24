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
            LoadingCanvas.target.Loading_Canvas.gameObject.SetActive(false);
            if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
            {
                TitleScreenLayer TitleScreenLayer = UILayerLoader.Load(NetFightScene.target.T, "TitleScreenLayer") as TitleScreenLayer;
                TitleScreenLayer.Initialise(FightOverControl.target.ReturnToFront);
            }else{
                NetFightScene.target.FightCanvas.gameObject.SetActive(true);
            }
            NetFightScene.target.PreparingCanvas.gameObject.SetActive(false);
            FightScenePauseSupport.target.ControlCanvas.gameObject.SetActive(true);
            NetFightScene.target.PressedStartButton();
        }
        
        public override void ProcessEnd()
        {
            MobileInputsManager.target.TurnOffButtons();
            if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
            {
                UILayerLoader.Remove("TitleScreenLayer");
            }
            else
            {
                NetFightScene.target.FightCanvas.gameObject.SetActive(false);
            }
            
            FightLogger.target.WatchMissionsAbandon();
        }

        public override void LocalUpdate()
        {
            RTFightManager.target.team1.localFightingUpdate();
            RTFightManager.target.team2.localFightingUpdate();
        }
    }
}