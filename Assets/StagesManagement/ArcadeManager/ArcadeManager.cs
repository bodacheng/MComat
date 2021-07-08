using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
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
        
        public void INIArcadeStageButtons()
        {
            List<Object> stageScriptableObjects = Resources.LoadAll("StageConfigFiles", typeof(FightInfo)).ToList();
            foreach (Object _object in stageScriptableObjects)
            {
                FightInfo one = (FightInfo)_object;
                one.eventType = FightEventType.Quest;
                if (!ArcadeStages.ContainsKey(one.LocalFightID))
                {
                    one.LoadLocalFightFromScript();
                    StageButton newButton = Instantiate(pretab);
                    void LoadThisStage()
                    {
                        ArcadeStages[one.LocalFightID].stageConfig.LoadMyTeam();
                        PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, ArcadeStages[one.LocalFightID].stageConfig, true);
                    }
                    newButton.button.onClick.AddListener(LoadThisStage);
                    newButton.ID = one.LocalFightID;
                    newButton.text.text = "Stage" + one.LocalFightID.ToString();
                    newButton.name = "Stage" + one.LocalFightID.ToString();

                    for (int i = 0; i < one.fightMembers.EnemySets.GetValues().Count; i++)
                    {
                        MonsterIconDic.Get(one.fightMembers.EnemySets.GetValues()[i].r_id);
                    }
                    List<HeroIcon> heroIcons = FightPreparePage.MemberInfosShow(one.fightMembers.EnemySets.GetValues(), newButton.IconsT);
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
                }
                else
                {
                    Debug.Log("重复的Arcade模式关卡ID：" + one.LocalFightID);
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