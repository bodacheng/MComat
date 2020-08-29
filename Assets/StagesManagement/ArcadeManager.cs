using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using dataAccess;
using System.Collections;
using UniRx;
using UnityEngine.SceneManagement;

namespace mainMenu
{
    public class ArcadeManager : MonoBehaviour
    {
        public Canvas _ArcadeCanvas;
        public RectTransform ButtonsContainer;
        
        [Space(7)]
        [Header("编辑队伍")]
        public Button EditTeamButton; // 根据进入战斗模式决定是否显示
        
        [Space(7)]
        [Header("编辑队伍")]
        public Button JumpToNewStage; // 根据进入战斗模式决定是否显示
        
        [Space(7)]
        [Header("myteamT")]
        public RectTransform myT;
        
        [Space(7)]
        [Header("ScrollRect")]
        public ScrollRect _ScrollRect;
        
        [Space(7)]
        [Header("ViewScrollBar")]
        public Scrollbar _Scrollbar;
        
        [Space(7)]
        [Header("StageButtonPretab")]
        public StageButton pretab;
        
        public static ArcadeManager target;
        //List<StageButton> stageButtons = new List<StageButton>();
        
        public static IDictionary<int, StageInfo> ArcadeStages = new Dictionary<int, StageInfo>();
        
        void Awake()
        {
            target = this;
        }
        
        public class StageInfo
        {
            public StageScriptableObject stageConfig;
            public StageButton stageButton;
            public List<HeroIcon> MemberIcons;
            
            public void ChangeColorOfIcons(bool on)
            {
                Image buttonImage = stageButton.GetComponent<Image>();
                if (on)
                {
                    buttonImage.raycastTarget = true;
                    buttonImage.color = new Color(1, 1, 1, 1);
                    stageButton.text.color = new Color(1, 1, 1, 1);
                    for (int i = 0; i < MemberIcons.Count; i++)
                    {
                        MemberIcons[i].LightOn();
                        MemberIcons[i].iconButton.targetGraphic.raycastTarget = false;
                    }
                }else{
                    buttonImage.raycastTarget = false;
                    buttonImage.color = new Color(1, 1, 1, 0.3f);
                    stageButton.text.color = new Color(1, 1, 1, 0.3f);
                    for (int i = 0; i < MemberIcons.Count; i++)
                    {
                        MemberIcons[i].Grey();
                        MemberIcons[i].iconButton.targetGraphic.raycastTarget = false;
                    }
                }
            }
        }
        
        public StageButton GetStageButton(int stageno)
        {
            return ArcadeStages[stageno]?.stageButton;
        }

        // 原则上这些玩意没有每次都去生成的道理..
        // 而且这个功能可能做一些扩展，比如关卡图标可以搞个特殊一类的
        // 2020523 : 计划根据账户进度选择是否显示隐藏关卡
        public IEnumerator GenerateStageButtons()
        {
            List<Object> stageScriptableObjects = Resources.LoadAll("StageConfigFiles", typeof(StageScriptableObject)).ToList();
            foreach (Object _object in stageScriptableObjects)
            {
                StageScriptableObject one = (StageScriptableObject)_object;
                
                if (!ArcadeStages.ContainsKey(one.LocalFightID))
                {
                    StageButton newButton = Instantiate(pretab);
                    void LoadThisStage()
                    {
                        FightLoad.PreLoad(ArcadeStages[one.LocalFightID].stageConfig, TeamSetGameMode.story);
                        FightLoad.GoTo();
                    }
                    newButton.button.onClick.AddListener(LoadThisStage);
                    newButton.ID = one.LocalFightID;
                    newButton.text.text = "Stage" + one.LocalFightID.ToString();
                    newButton.name = "Stage" + one.LocalFightID.ToString();
                    one.LoadLocalFightFromScript();
                    for (int i = 0; i < one.localFight.EnemySets.values.Count; i++)
                    {
                        IEnumerator onecoroutine = MonsterIconDic.Instance.LoadAndGet(one.localFight.EnemySets.values[i].ResourceID);
                        yield return onecoroutine;
                    }
                    List<HeroIcon> heroIcons = FightPreparePage.MemberInfosShow(one.localFight.EnemySets.values, newButton.GetComponent<RectTransform>());
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
            
             // 假设有100关，然后按钮应该是越往下拖关卡数越大，才能和JumpToNewest()堆起来
            for (int i = 100; i > -1; i--)
            {
                if (!ArcadeStages.ContainsKey(i))
                {
                    continue;
                }
                ArcadeStages[i].stageButton.gameObject.SetActive(true);
                ArcadeStages[i].stageButton.gameObject.transform.SetParent(ButtonsContainer);
                ArcadeStages[i].stageButton.gameObject.transform.localScale = Vector3.one;
            }
            VerticalLayoutGroup verticalLayoutGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
            ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x, (pretab.button.GetComponent<RectTransform>().rect.height + verticalLayoutGroup.spacing) * ArcadeStages.Count);
            
            EditTeamButton.onClick.RemoveAllListeners();
            EditTeamButton.onClick.AddListener(TeamSet.GoToTeamEdit_Arcade);
            JumpToNewStage.onClick.RemoveAllListeners();
            JumpToNewStage.onClick.AddListener(JumpToNewest);

            SingleAssignmentDisposable autoHide = new SingleAssignmentDisposable();
            autoHide = new SingleAssignmentDisposable
            {
                Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (FightGlobalSetting.scenestep != 1)
                        {
                            autoHide.Dispose();
                        }
                        if (!JumpToNewStage.gameObject.activeSelf)
                        {
                            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) > 0.2f)
                            {
                                JumpToNewStage.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(CurrentTargetScrollbarValue() - _Scrollbar.value) <= 0.2f)
                            {
                                JumpToNewStage.gameObject.SetActive(false);
                            }
                        }
                    }
                )
            };

            PosKeySet set = TeamSet.Default;
            IEnumerator getDefaultTeamSet = TeamSet.MyTeamByEntryLimit(4, set);// 暂时不根据关卡人数限制修改我方队伍显示
            yield return getDefaultTeamSet;
            if (getDefaultTeamSet.Current == null)
            {
                Debug.Log("获取我方人员错误");
                yield break;
            }
            MultiDictionary<int, int, CharDataInfo> my = (MultiDictionary<int, int, CharDataInfo>)getDefaultTeamSet.Current;
            for (int i = 0; i < my.values.Count; i++)
            {
                IEnumerator onecoroutine = MonsterIconDic.Instance.LoadAndGet(my.values[i].ResourceID);
                yield return onecoroutine;
            }
            FightPreparePage.MemberInfosShow(my.values, myT);

            yield break;
        }
        
        // Button feature
        public void JumpToNewest()
        {
            JumpTo(AccountSet._AccInfo.ArcadeProcess);
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
        
        public void JumpTo(int stageNum)
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
        
        public static void PreventStageButtonsFromDestroy()
        {
            foreach (KeyValuePair<int, StageInfo> keyValuePair in ArcadeStages)
            {
                keyValuePair.Value.stageButton.transform.SetParent( ResourceKeeper.dontDestroyOnLoadParent);
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