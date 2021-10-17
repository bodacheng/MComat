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
            return FightOverControl.target.logger.GameOver.Value;
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
                TitleScreenLayer TitleScreenLayer = UILayerLoader.Load(NetFightScene.target.T.gameObject, "TitleScreenLayer") as TitleScreenLayer;
                TitleScreenLayer.Initialise(FightOverControl.target.ReturnToFront);
            }else{
                NetFightScene.target.FightCanvas.gameObject.SetActive(true);
            }
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
            
            FightOverControl.target.logger.WatchMissionsAbandon();
        }

        public override void LocalUpdate()
        {
            RTFightManager.target.team1.localFightingUpdate(RTFightManager.target.Team1Members);
            RTFightManager.target.team2.localFightingUpdate(RTFightManager.target.Team2Members);
        }
    }
}