using System;
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using DummyLayerSystem;
using mainMenu;
using UnityEngine.SceneManagement;

namespace FightScene
{
    public class FightScene : MonoBehaviour
    {
        [SerializeField] RectTransform T;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource uiAudioSource;
        
        [Header("FX")]
        public Camera fxCamera;
        
        [SerializeField] RewardedAdsButton watchAdBtnPrefab;

        public static FightScene target;
        
        public ReactiveProperty<bool> LoadStageFinished { get; set; } = new ReactiveProperty<bool>(false);
        
        public static List<GangbangInfo.SoldierGroupSet> team1GroupSet;
        
        private RewardedAdsButton watchBtn;
        public void ShowAds(int extraAdReward, RectTransform btnTarget, Action afterWatched, int finishedStage = -1)
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
                        CloudScript.RequestAdReward(
                            "DM",
                            extraAdReward, 
                            afterWatched,
                            finishedStage
                        );
                    }
                );
                watchBtn.gameObject.SetActive(true);
            }
        }

        void Awake()
        {
            target = this;
        }
        
        void Start()
        {
            UILayerLoader.Clear();
            UILayerLoader.SetHanger(T);
            
            //HighLightLayer.DarkOff(Color.white, 0, true);
            Time.timeScale = 1;
            if (FightLoad.Fight == null)
            {
                return;
            }
            
            AppSetting.BGMSource = audioSource;
            AppSetting.UiAudioSource = uiAudioSource;
            Application.targetFrameRate = 60;
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
                case FightEventType.Gangbang:
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

        public void LoadAds()
        {
            watchBtn = Instantiate(watchAdBtnPrefab);
            watchBtn.LoadAd();
            watchBtn.gameObject.SetActive(false);
            watchBtn.transform.SetParent(transform);
        }
        
        void Update()
        {
            FSceneProcessesRunner.Main.ProcessNagare();
            TutorialRunner.Main.Process();
        }
        
        public void ReturnToFront()
        {
            FSceneProcessesRunner.Main.ChangeProcess(SceneStep.None);
            var dataCenters = new List<Data_Center>();
            dataCenters.AddRange(RTFightManager.Target.team1.teamMembers.GetValues());
            dataCenters.AddRange(RTFightManager.Target.team2.teamMembers.GetValues());
            RTFightManager.Target.ClearUnitData();
            RTFightManager.Target.team1.Clear();
            RTFightManager.Target.team2.Clear();
            FightLogger.value.WatchMissionsAbandon();
            FSceneProcessesRunner.Main.Clear();
            if (FightLoad.Fight.EventType == FightEventType.Quest)
                ProcessesRunner.Main.Clear();
            MainMenuNote.GoingTo = MainSceneStep.FrontPage;
            HitBoxesProcesser.Instance.Clear();
            SingleAssignmentDisposableCleaner.Clear();
            SceneManager.LoadScene(1);
        }
    }
}