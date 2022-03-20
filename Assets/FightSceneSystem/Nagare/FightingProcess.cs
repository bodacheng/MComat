using DummyLayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using mainMenu;

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
            //CameraMode nowC = RealTimeGameProcessManager.target._CameraManager.CModeDic[C_Mode.OneVOne];
            //if (nowC is OneVOneMode)
            //{
            //    DOTween.To(() => ((OneVOneMode)nowC).xzMax, (x) => ((OneVOneMode)nowC).xzMax = x, 16, 3f);
            //}
            
            _layer = FightingStepLayer.Get();
            
            if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
            {
                var TitleScreenLayer = UILayerLoader.Load(NetFightScene.target.T.gameObject, "TitleScreenLayer") as TitleScreenLayer;
                TitleScreenLayer.Initialise(NetFightScene.target.ReturnToFront, 
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
            if (NetFightScene.Fight.GetEventType() == FightEventType.Screensaver)
            {
                UILayerLoader.Remove("TitleScreenLayer");
            }
            else
            {
                UILayerLoader.Remove("FightingStepLayer");
            }
            RTFightManager.target.ClearUnitData();
            _layer.Clear();
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