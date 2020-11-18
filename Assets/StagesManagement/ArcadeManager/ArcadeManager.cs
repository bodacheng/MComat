using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using dataAccess;
using System.Collections;
using UniRx;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
        public Canvas _ArcadeCanvas;
        public RectTransform ButtonsContainer;
        
        [Space(7)]
        [Header("编辑队伍")]
        public Button JumpToNewStage; // 根据进入战斗模式决定是否显示
        
        [Space(7)]
        [Header("ScrollRect")]
        public ScrollRect _ScrollRect;
        
        [Space(7)]
        [Header("ViewScrollBar")]
        public Scrollbar _Scrollbar;
        
        [Space(7)]
        [Header("StageButtonPretab")]
        public StageButton pretab;
        
        [Space(7)]
        [Header("mini nineslot")]
        public NineForShow _NineForShow;
        
        public static ArcadeManager target;
        //List<StageButton> stageButtons = new List<StageButton>();

        public static IDictionary<int, StageInfo> ArcadeStages = new Dictionary<int, StageInfo>();

        SingleAssignmentDisposable autoHide;
                
        void Awake()
        {
            target = this;
        }
        
        public StageButton GetStageButton(int stageno)
        {
            return ArcadeStages[stageno]?.stageButton;
        }
        
        // 原则上这些玩意没有每次都去生成的道理..
        // 而且这个功能可能做一些扩展，比如关卡图标可以搞个特殊一类的
        // 2020523 : 计划根据账户进度选择是否显示隐藏关卡
        public IEnumerator INIArcadeStageButtons()
        {
            List<Object> stageScriptableObjects = Resources.LoadAll("StageConfigFiles", typeof(StageScriptableObject)).ToList();
            foreach (Object _object in stageScriptableObjects)
            {
                StageScriptableObject one = (StageScriptableObject)_object;
                if (!ArcadeStages.ContainsKey(one.LocalFightID))
                {
                    one.LoadLocalFightFromScript();
                    StageButton newButton = Instantiate(pretab);
                    void LoadThisStage()
                    {
                        FightLoad.PreLoad(ArcadeStages[one.LocalFightID].stageConfig, TeamSetGameMode.story);
                        PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo,true);
                    }
                    newButton.button.onClick.AddListener(LoadThisStage);
                    newButton.ID = one.LocalFightID;
                    newButton.text.text = "Stage" + one.LocalFightID.ToString();
                    newButton.name = "Stage" + one.LocalFightID.ToString();
                    
                    for (int i = 0; i < one.localFight.EnemySets.values.Count; i++)
                    {
                        IEnumerator onecoroutine = MonsterIconDic.Instance.LoadAndGet(one.localFight.EnemySets.values[i].ResourceID);
                        yield return onecoroutine;
                    }
                    List<HeroIcon> heroIcons = FightPreparePage.MemberInfosShow(one.localFight.EnemySets.values, newButton.IconsT);
                    for (int i = 0; i < heroIcons.Count; i++)
                    {
                        HeroIcon heroIcon = heroIcons[i];
                        void temp()
                        {
                            IconButtonFeature(heroIcon);
                        }
                        heroIcon.iconButton.onClick.RemoveAllListeners();
                        heroIcon.iconButton.onClick.AddListener(temp);
                    }
                    
                    StageInfo stageInfo = new StageInfo
                    {
                        stageConfig = one,
                        stageButton = newButton,
                        MemberIcons = heroIcons
                    };
                    ArcadeStages.Add(one.LocalFightID, stageInfo);
                }else{
                    Debug.Log("重复的Arcade模式关卡ID："+ one.LocalFightID);
                }
            }
            
            autoHide = new SingleAssignmentDisposable
            {
                Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (FightGlobalSetting.scenestep != 0 || JumpToNewStage.IsDestroyed() || JumpToNewStage == null || JumpToNewStage.gameObject == null)
                        {
                            autoHide.Dispose();
                            return;
                        }
                        if (!JumpToNewStage.gameObject.activeSelf)
                        {
                            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) > 0.1f)
                            {
                                JumpToNewStage.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) <= 0.1f)
                            {
                                JumpToNewStage.gameObject.SetActive(false);
                            }
                        }
                    }
                )
            };
        }
        
        public void IconButtonFeature(HeroIcon heroIcon)
        {
            // 显示模型
            MemberDetail.target.presentationProcessRunner.Run(ModelShower.target.ShowModel(heroIcon._CharConfig.RECORD_ID));
            // 显示技能组
            MemberDetail.target.presentationProcessRunner.Run(_NineForShow.ShowStones_DataInfo(heroIcon.CharDataInfo));
        }
        
        float CurrentTargetScrollbarValue()
        {
            float targetScrollbarValue;
            if (AccountSet._AccInfo.ArcadeProcess <= 3)
            {
                targetScrollbarValue = 0;
            }else{
                VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
                // 重点在于对Scrollbar.value的理解。这个值是scrollview边界目前超出框的长度与可能超出框框最大长度的比值
                targetScrollbarValue = 
                ((pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * (AccountSet._AccInfo.ArcadeProcess - 3)) // 分子。如果希望对象关卡不是出现在中间，可调整这个数字。
                / (ButtonsContainer.sizeDelta.y - _ScrollRect.GetComponent<RectTransform>().rect.height); // 分母
            }
            return targetScrollbarValue;
        }
        
        public void JumpTo()
        {
            DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value= x, CurrentTargetScrollbarValue(), 0.5f);
        }
        
        public void RefreshRender()
        {
            foreach (KeyValuePair<int, StageInfo> keyValuePair in ArcadeStages)
            {
                Image buttonImage = keyValuePair.Value.stageButton.GetComponent<Image>();
                Animator buttonAnimator = keyValuePair.Value.stageButton.GetComponent<Animator>();
                if (buttonAnimator != null)
                    buttonAnimator.enabled = AccountSet._AccInfo.ArcadeProcess == keyValuePair.Key;
                if (AccountSet._AccInfo.ArcadeProcess >= keyValuePair.Key)
                {
                    keyValuePair.Value.ChangeColorOfIcons(true);
                }else{
                    keyValuePair.Value.ChangeColorOfIcons(false);
                }
            }
        }
                
        // Button feature
        public void BeginNewStage()
        {
            ArcadeStages.TryGetValue(AccountSet._AccInfo.ArcadeProcess, out StageInfo targetStage);
            if (targetStage != null)
            {
                FightLoad.PreLoad(targetStage.stageConfig, TeamSetGameMode.story);
                FightLoad.GoTo();
            }
        }
               
        // 等级升序降序
        //readonly int order = 0;//0:升序 1:降序 //是否按type排序
        //List<StageButton> OrderStagesButtonByNo(List<StageButton> originBoxes)
        //{
        //    for (int i = 0; i < originBoxes.Count - 1; i++)
        //    {
        //        for (int j = 0; j < originBoxes.Count - 1 - i; j++)
        //        {
        //            int no1 = originBoxes[j].ID;
        //            int no2 = originBoxes[j + 1].ID;
        //            if (order == 1 ? no1 > no2 : no1 < no2)
        //            {
        //                StageButton temp = originBoxes[j];
        //                originBoxes[j] = originBoxes[j + 1];
        //                originBoxes[j + 1] = temp;
        //            }
        //        }
        //    }
        //    return originBoxes;
        //}
    }
}