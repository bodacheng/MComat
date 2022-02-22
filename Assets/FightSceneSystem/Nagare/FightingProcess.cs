using UnityEngine;
using UnityEngine.SceneManagement;
using mainMenu;

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
            if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
            {
                TitleScreenLayer TitleScreenLayer = UILayerLoader.Load(NetFightScene.target.T.gameObject, "TitleScreenLayer") as TitleScreenLayer;
                TitleScreenLayer.Initialise(FightOverControl.target.ReturnToFront, 
                    () =>
                    {
                        LoginLayer LoginLayer = LoginLayer.Open(
                            result => {
                                Debug.Log(" 登陆成功，获得下面这样一个东西： " + result.EntityToken.EntityToken);
                                PlayerAccountInfo.Me = new PlayerAccountInfo
                                {
                                    PlayFabUsername = result.PlayFabId
                                };
                                CloudScript.CheckIn();
                                MainMenuNote.goingtostep = MainSceneStep.FrontPage;
                                SceneManager.LoadScene(1);
                            },
                            fail => {
                                Debug.Log("login fail");
                            }
                        );
                    });
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
                UILayerLoader.Remove("FightingStepLayer");
            }
            RTFightManager.target.Clear();
            FightOverControl.target.logger.WatchMissionsAbandon();
        }

        public override void LocalUpdate()
        {
            RTFightManager.target.team1.localUpdate(RTFightManager.target.Team1Members);
            RTFightManager.target.team2.localUpdate(RTFightManager.target.Team2Members);
        }
    }
}