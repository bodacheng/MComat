using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DummyLayerSystem;
using mainMenu;
using UnityEngine.SceneManagement;

namespace FightScene
{
    public class FightScene : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        [SerializeField] RectTransform safeAreaRect;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource uiAudioSource;
        
        [Header("FX")]
        public Camera fxCamera;
        
        [SerializeField] SensorUnity sensorUnity;
        public SensorUnity SensorUnity => sensorUnity;
        
        [SerializeField] AdmobAdsButton watchAdBtnPrefab;
        
        [SerializeField] private AIServiceManager aiServiceManager;
        
        public AIServiceManager AIServiceManager => aiServiceManager;
        private StoryInfo aiStoryInfo;
        private string aiStoryRequestKey;
        private UniTaskCompletionSource<StoryInfo> aiStoryLoadSource;
        public StoryInfo AIStoryInfo => aiStoryInfo;
        
        public static FightScene target;
        
        public ReactiveProperty<bool> LoadStageFinished { get; set; } = new ReactiveProperty<bool>(false);
        
        public static List<FightInfo.SoldierGroupSet> team1GroupSet;
        
        private AdmobAdsButton watchBtn;
        public void ShowAds(int extraAdReward, RectTransform btnTarget, Action afterWatched, int finishedStage = -1, bool showAdImmediately = true)
        {
            if (extraAdReward > 0 && watchBtn != null)
            {
                watchBtn.transform.SetParent(btnTarget);
                watchBtn.transform.localPosition = Vector3.zero;
                
                string awardText = "x2"; // 简化处理 
                watchBtn.Text = awardText;
                watchBtn.SetWatchedAdExtraProcess(
                    () =>
                    {
                        watchBtn.ShowAdButton.gameObject.SetActive(false);
                        CloudScript.RequestAdReward(
                            "DM",
                            extraAdReward, 
                            afterWatched,
                            finishedStage
                        );
                    }
                );
                if (watchBtn.AdIsReady && showAdImmediately)
                {
                    watchBtn.ShowAd();
                }
                watchBtn.gameObject.SetActive(true);
            }
        }
        
        public void JustShowAds()
        {
            if (watchBtn != null)
            {
                if (watchBtn.AdIsReady)
                {
                    watchBtn.ShowAd();
                }
            }
        }

        void Awake()
        {
            target = this;
            PosCal.Canvas = this.canvas;
            PosCal.SafeAreaRect = safeAreaRect;
            PosCal.TestIni();
        }
        
        void Start()
        {
            UILayerLoader.Clear();
            UILayerLoader.SetHanger(safeAreaRect, canvas.transform);
            
            //HighLightLayer.DarkOff(Color.white, 0, true);
            Time.timeScale = 1;
            if (FightLoad.Fight == null)
            {
                return;
            }
            
            PrepareStoryContentForCurrentFight();
            
            AppSetting.BGMSource = audioSource;
            AppSetting.UiAudioSource = uiAudioSource;
            QualitySettings.vSyncCount = 0; // 关闭 VSync
            Application.targetFrameRate = 70;
            FightGlobalSetting.SceneStep = 1;
            
            //Position_Set_Executor.Instance.P_sets.Clear();
            var preparingProcess = new PreparingProcess();
            var countDownProcess = new CountDownProcess();
            var fightingProcess = new FightingProcess();
            var fightResultAnim = new FightResultAnim();
            var fightOverProcess = new FightOverProcess();
            
            FSceneProcessesRunner.Main.Clear();
            switch(FightLoad.Fight.EventType)
            {
                case FightEventType.Arena:
                case FightEventType.Quest:
                case FightEventType.Event:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightResultAnim, fightResultAnim);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
                    break;
                case FightEventType.SkillTest:
                case FightEventType.Self:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.CountDown, countDownProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.FightOver, fightOverProcess);
                    break;
                case FightEventType.Screensaver:
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Preparing, preparingProcess);
                    FSceneProcessesRunner.Main.AddNewProcess(SceneStep.Fighting, fightingProcess);
                    break;
            }
            FSceneProcessesRunner.Main.ArrangeProcessOrder();
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.Preparing);
        }

        public void PrepareStoryContentForCurrentFight()
        {
            ArenaFightOver.PreloadQuestShortStoryIfNeeded();
            PrepareAIStoryForCurrentFight();
        }

        public void PrepareAIStoryForCurrentFight()
        {
            var fight = FightLoad.Fight;
            if (!ShouldLoadAIStory(fight))
            {
                aiStoryInfo = null;
                aiStoryRequestKey = null;
                aiStoryLoadSource = null;
                return;
            }

            var requestKey = BuildAIStoryRequestKey(fight);
            if (aiStoryLoadSource != null && aiStoryRequestKey == requestKey)
            {
                return;
            }

            aiStoryInfo = null;
            aiStoryRequestKey = requestKey;
            aiStoryLoadSource = new UniTaskCompletionSource<StoryInfo>();
            LoadAIStoryForCurrentFight(aiStoryLoadSource, requestKey).Forget();
        }

        public async UniTask<StoryInfo> GetAIStoryForCurrentFightAsync()
        {
            if (aiStoryInfo != null)
            {
                return aiStoryInfo;
            }

            if (!ShouldLoadAIStory(FightLoad.Fight))
            {
                return null;
            }

            PrepareAIStoryForCurrentFight();
            var loadSource = aiStoryLoadSource;
            return loadSource == null ? null : await loadSource.Task;
        }

        private async UniTaskVoid LoadAIStoryForCurrentFight(UniTaskCompletionSource<StoryInfo> loadSource, string requestKey)
        {
            StoryInfo story = null;
            try
            {
                if (aiServiceManager == null)
                {
                    Debug.LogWarning("[FightScene] AIServiceManager missing, cannot load AI story.");
                }
                else
                {
                    story = await aiServiceManager.LoadAIStory();
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[FightScene] Failed to load AI story: {e.Message}");
            }

            if (aiStoryLoadSource == loadSource && aiStoryRequestKey == requestKey)
            {
                aiStoryInfo = story;
            }

            loadSource.TrySetResult(story);
        }

        private static bool ShouldLoadAIStory(FightInfo fight)
        {
            if (fight == null)
            {
                return false;
            }

            var hasDefaultStory = !string.IsNullOrEmpty(fight.StoryKey);
            return fight.EventType == FightEventType.Event ||
                   (fight.EventType == FightEventType.Quest && !hasDefaultStory);
        }

        private static string BuildAIStoryRequestKey(FightInfo fight)
        {
            return fight == null
                ? string.Empty
                : $"{fight.EventType}:{fight.FightMode}:{fight.ID}";
        }

        public void LoadAds()
        {
            if (!AdsInitializer.ShouldEnableAds())
            {
                return;
            }

            watchBtn = Instantiate(watchAdBtnPrefab);
            watchBtn.HasTicket = true;
            watchBtn.LoadAd();
            watchBtn.gameObject.SetActive(false);
            watchBtn.transform.SetParent(transform);
        }
        
        void Update()
        {
            FSceneProcessesRunner.Main.ProcessNagare();
        }
        
        void FixedUpdate()
        {
            FSceneProcessesRunner.Main.ProcessFixedUpdate();
        }
        
        public void ReturnToFront(MainSceneStep mainSceneStep = MainSceneStep.FrontPage)
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            var cameraManager = RTFightManager.Target?._CameraManager;
            if (cameraManager != null)
            {
                // 防止切回主场景过程中继续执行战斗相机逻辑
                cameraManager.Assign_Camera(C_Mode.NULL, null, null);
            }
            RTFightManager.Target.ClearUnitData();
            RTFightManager.Target.team1.Clear();
            RTFightManager.Target.team2.Clear();
            FightLogger.value.WatchMissionsAbandon();
            FSceneProcessesRunner.Main.Clear();
            if (FightLoad.Fight.EventType == FightEventType.Quest)
                ProcessesRunner.Main.Clear();
            MainMenuNote.GoingTo = mainSceneStep;
            HitBoxesProcesser.Instance.Clear();
            SingleAssignmentDisposableCleaner.Clear();
            SceneManager.LoadScene(1);
        }
    }
}
