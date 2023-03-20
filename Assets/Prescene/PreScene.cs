using Cysharp.Threading.Tasks;
using UnityEngine;
using DummyLayerSystem;
using ModelView;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace mainMenu
{
    public class PreScene : MonoBehaviour
    {
        public static PreScene target;
        
        public Camera postProcessCamera;
        public Camera noPostProcessCamera;
        
        [Header("T")]
        [SerializeField] GameObject T;
        [Header("Shader转换器")]
        [SerializeField] SwapAllModelShader _SwapAllModelShader;
        [Header("AudioSource")]
        [SerializeField] AudioSource audioSource;

        public RectTransform stonesTempContainer;

        public UnitInfo Focusing => _focusing;
        UnitInfo _focusing;
        
        public void SetFocusingUnit(string instanceID)
        {
            _focusing = dataAccess.Units.Get(instanceID);
            if (_focusing == null)
            {
                BackGroundPS.target.ChangeBGByElement(Element.Null);
                return;
            }
            
            var unitConfig = Units.GetUnitConfig(_focusing.r_id);
            if (unitConfig == null)
            {
                Debug.Log("Error r id:" + _focusing.r_id);
                return;
            }
            BackGroundPS.target.ChangeBGByElement(unitConfig.element);
        }

        [SerializeField] private RawImage effectBg;
        RenderTexture effectRenderTexture;
        void Awake()
        {
            target = this;
            SetBgRenderTexture();
        }
        
        void SetBgRenderTexture()
        {
            effectRenderTexture = new RenderTexture(Screen.width, Screen.height, 16);
            effectRenderTexture.Create();
            noPostProcessCamera.targetTexture = effectRenderTexture;
            effectBg.texture = effectRenderTexture;
            effectBg.color = Color.white;
        }
        
        // URP的postprocess有个默认设置，就是overlay的相机如果启用postprocess，
        // 那么它会把被叠加的主相机和在它之前的stack全都加上postprocess，哪怕这些相机culling mask不一样
        // 甚至复数个stack都有postprocess的话还能把前面前面的stack或base的东西postprocess给增强
        public void CameraStackToPostProcess(Camera camera)
        {
            var pCameraData = postProcessCamera.GetComponent<UniversalAdditionalCameraData>();
            if (pCameraData.cameraStack.Contains(camera))
                return;
            
            pCameraData.renderPostProcessing = false;
            for (var index = 0; index < pCameraData.cameraStack.Count; index++)
            {
                var stackCamera = pCameraData.cameraStack[index];
                var sCameraData = stackCamera.GetComponent<UniversalAdditionalCameraData>();
                sCameraData.renderPostProcessing = false;
            }
            
            var cameraData = camera.transform.GetComponent<UniversalAdditionalCameraData>();
            cameraData.renderType = CameraRenderType.Overlay;
            cameraData.renderPostProcessing = true;
            pCameraData.cameraStack.Add(camera);
            OnDestroyCallback.AddOnDestroyCallback(camera.gameObject, () =>
            {
                pCameraData.cameraStack.Remove(camera);
                pCameraData.renderPostProcessing = pCameraData.cameraStack.Count == 0;
                for (var index = 0; index < pCameraData.cameraStack.Count; index++)
                {
                    var stackCamera = pCameraData.cameraStack[index];
                    if (stackCamera != null)
                    {
                        var sCameraData = stackCamera.GetComponent<UniversalAdditionalCameraData>();
                        sCameraData.renderPostProcessing = index == pCameraData.cameraStack.Count - 1;
                    }
                }
            });
        }
        
        public void CameraStackToNonePostProcess(Camera camera)
        {
            var pCameraData = noPostProcessCamera.GetComponent<UniversalAdditionalCameraData>();
            if (pCameraData.cameraStack.Contains(camera))
                return;
            var cameraData = camera.transform.GetComponent<UniversalAdditionalCameraData>();
            cameraData.renderType = CameraRenderType.Overlay;
            cameraData.renderPostProcessing = false;
            pCameraData.cameraStack.Add(camera);
        }

        void Start()
        {
            AnimationResourceLoader.Instance.Clear();
            DedicatedCameraConnector.ClearBackUpModels();
            HurtObjectManager.Clear();
            EffectsManager.Clear();
            
            Screen.SetResolution(1920, 1080, true);
            UILayerLoader.Clear();
            UILayerLoader.SetHanger(T.transform);
            UILayerLoader.SetEffectBg(effectBg.rectTransform);
            AppSetting.BGMSource = audioSource;
            AppSetting.PlayBGM(CommonSetting.LobbyThemeAddressKey).Forget();
            Time.timeScale = 1;
            FightGlobalSetting.SceneStep = 0;
            
            BasicPhase();
            ToInitialPhase();
            
            AddressablesLogic.Essentials().Forget();
        }
        
        public static void ReturnToLobby()
        {
            ProgressLayer.Loading(">");
            PopupLayer.ArrangeConfirmWindow((() => { SceneManager.LoadScene(0);}), "Network Error. Return to lobby.");
        }
        
        void BasicPhase()
        {
            Application.targetFrameRate = 60;
            
            #region 主界面各大画面
            var settingPage = new SettingPage();
            var frontPage = new FrontPage();
            var teamEditFront = new TeamEditPage();
            var skillStones = new StonesPage();
            var stoneSell = new StoneSell();
            var selfFightFront = new SelfFightPage();
            var questInfo = new QuestInfoPage();
            var unitListPage = new UnitListPage();
            var memberDetailEdit = new SkillEditPage();
            var memberDetailSkillShow = new SkillShowPage();
            var arcadeFrontPage = new ArcadeFrontPage();
            
            // Shop
            var shopTop = new ShopTop();

            // Gotcha
            var gotchaFront = new GotchaFront();
            var gotchaResult = new GotchaResult();
            var dropTableInfo = new DropTableInfoDetail();
            var arenaPage = new ArenaPage();
            var rankingPage = new RankingPage();
            
            // mail
            var mailBox = new MailBoxProcess();
            var mailDetail = new MailDetailProcess();
            
            ProcessesRunner.Main.Clear();
            ProcessesRunner.Main.Add(MainSceneStep.Setting, settingPage);
            ProcessesRunner.Main.Add(MainSceneStep.TeamEditFront, teamEditFront);
            ProcessesRunner.Main.Add(MainSceneStep.SkillStoneList, skillStones);
            ProcessesRunner.Main.Add(MainSceneStep.SkillStones_Sell, stoneSell);
            ProcessesRunner.Main.Add(MainSceneStep.SelfFightFront, selfFightFront);
            ProcessesRunner.Main.Add(MainSceneStep.QuestInfo, questInfo);
            ProcessesRunner.Main.Add(MainSceneStep.UnitList, unitListPage);
            ProcessesRunner.Main.Add(MainSceneStep.UnitSkillEdit, memberDetailEdit);
            ProcessesRunner.Main.Add(MainSceneStep.UnitSkillShow, memberDetailSkillShow);
            ProcessesRunner.Main.Add(MainSceneStep.FrontPage, frontPage);
            ProcessesRunner.Main.Add(MainSceneStep.ArcadeFront, arcadeFrontPage);
            ProcessesRunner.Main.Add(MainSceneStep.Arena, arenaPage);
            ProcessesRunner.Main.Add(MainSceneStep.Ranking, rankingPage);
            ProcessesRunner.Main.Add(MainSceneStep.ShopTop, shopTop);
            ProcessesRunner.Main.Add(MainSceneStep.MailBox, mailBox);
            ProcessesRunner.Main.Add(MainSceneStep.MailDetail, mailDetail);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaFront, gotchaFront);
            ProcessesRunner.Main.Add(MainSceneStep.GotchaResult, gotchaResult);
            ProcessesRunner.Main.Add(MainSceneStep.DropTableInfo, dropTableInfo);
            #endregion
        }
        
        void ToInitialPhase()
        {
            if (ReturnLayer.ReturnMissionList.Count > 0)
            {
                //ReturnLayer.AddFeatureToReturnButton();
                //从战斗画面返回后，进入战斗前的菜单往上跳一节，指的是站前准备画面
                ReturnLayer.POP();
            }
            else
            {
                trySwitchToStep(MainMenuNote.GoingTo, false);
            }
        }
        
        void Update()
        {
            ProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.Process();
        }

        public void AskIfLoadFight(FightInfo stage)
        {
            PopupLayer.ArrangeConfirmWindow(
                delegate {
                    FightLoad.Go(stage, true);
                }, "开打？");
        }

        public void BeginSkillTest_Rotation()
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.Rotation);
            stage.Team1ID = PlayerAccountInfo.Me.PlayFabId;
            FightLoad.Go(stage);
        }
        
        public void BeginSkillTest_Multi()
        {
            var stage = FightInfo.RandomSkillTestStage(TeamMode.MultiRaid);
            stage.Team1ID = PlayerAccountInfo.Me.PlayFabId;
            FightLoad.Go(stage);
        }
        
        [EnumAction(typeof(MainSceneStep))]
        public bool trySwitchToStep(MainSceneStep nextStep, bool forward = true)
        {
            if (forward && ProcessesRunner.Main.currentProcess != null)
            {
                var returnToStep = ProcessesRunner.Main.currentProcess.Step;
                bool ReturnToCurrent()
                {
                    return trySwitchToStep(returnToStep, false);
                }
                
                var success = ProcessesRunner.Main.ChangeProcess(nextStep);
                if (success)
                    ReturnLayer.PUSH(ReturnToCurrent);
                return success;
            }
            else
            {
                return ProcessesRunner.Main.ChangeProcess(nextStep);
            }
        }
        
        public bool ReEnterCurrent()
        {
            return ProcessesRunner.Main.ChangeProcess(ProcessesRunner.Main.currentProcess.Step);
        }
        
        public bool trySwitchToStep<T>(MainSceneStep nextStep, T t, bool forward)
        {
            if (forward && ProcessesRunner.Main.currentProcess != null)
            {
                var returnToStep = ProcessesRunner.Main.currentProcess.Step;
                bool ReturnToCurrent()
                {
                    Debug.Log("返回："+ returnToStep);
                    return trySwitchToStep(returnToStep, false);
                }
                var success = ProcessesRunner.Main.ChangeProcess(nextStep, t);
                if (success)
                    ReturnLayer.PUSH(ReturnToCurrent);
                return success;
            }
            else
            {
                return ProcessesRunner.Main.ChangeProcess(nextStep, t);
            }
        }
    }
}