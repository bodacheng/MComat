using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public static IDictionary<int, StageInfo> ArcadeStages = new Dictionary<int, StageInfo>();
        SingleAssignmentDisposable autoCommand;

        int StageCount;
        
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
                        FightLoad.PreLoad(ArcadeStages[one.LocalFightID].stageConfig, "arcade");
                        PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo,true);
                    }
                    newButton.button.onClick.AddListener(LoadThisStage);
                    newButton.ID = one.LocalFightID;
                    newButton.text.text = "Stage" + one.LocalFightID.ToString();
                    newButton.name = "Stage" + one.LocalFightID.ToString();
                    
                    for (int i = 0; i < one.localFight.EnemySets.values.Count; i++)
                    {
                        IEnumerator onecoroutine = MonsterIconDic.LoadAndGet(one.localFight.EnemySets.values[i].ResourceID);
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
            StageCount = ArcadeStages.Count;
        }
        
        public void IconButtonFeature(HeroIcon heroIcon)
        {
            // 显示模型
            MemberDetail.target.presentationProcessRunner.RunAsQueued(ModelShower.target.ShowModel(heroIcon._CharConfig.RECORD_ID));
            // 显示技能组
            _NineForShow.ShowStones_DataInfo(heroIcon.CharDataInfo);
        }
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