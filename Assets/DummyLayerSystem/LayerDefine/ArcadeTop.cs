using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using mainMenu;
using dataAccess;
using System;
using DG.Tweening;

public partial class ArcadeTop : UILayer
{
    [SerializeField] RectTransform ButtonsContainer;
    [SerializeField] Button JumpToNewStage;
    [SerializeField] ScrollRect _ScrollRect;
    [SerializeField] Scrollbar _Scrollbar;
    [SerializeField] StageButton iconPrefab;
    [SerializeField] HeroIcon UnitIconPrefab;
    [SerializeField] NineForShow _NineForShow;
    
    int StageCount; 
    public readonly IDictionary<int, StageInfo> ArcadeStages = new Dictionary<int, StageInfo>();
    
    public static ArcadeTop Open()
    {
        var returnValue = UILayerLoader.Load(PreScene.target.T,"ArcadeTop") as ArcadeTop;
        returnValue.INIArcadeStageButtons();
        returnValue.INIPagingSystem(2);
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("ArcadeTop");
    }
    
    public void IconButtonFeature(HeroIcon heroIcon)
    {
        // 显示模型
        SingleThreadProcesser.backup.RunAsQueued(ModelShower.target.ShowModel(heroIcon.unitConfig.RECORD_ID));
        // 显示技能组
        _NineForShow.ShowStones_DataInfo(heroIcon.unitInfo);
    }

    void INIArcadeStageButtons()
    {
        var stageResources = Resources.LoadAll("StageConfigFiles", typeof(FightInfo)).ToList();
        foreach (var _object in stageResources)
        {
            var one = (FightInfo)_object;
            one.ID = int.Parse(one.name);
            one.SetEventType(FightEventType.Quest);
            if (!ArcadeStages.ContainsKey(one.ID))
            {
                one.LoadLocalFightFromScript();
                var newButton = Instantiate(iconPrefab);
                void LoadThisStage()
                {
                    ArcadeStages[one.ID].stageConfig.LoadMyTeam();
                    ArcadeStages[one.ID].stageConfig.team1ID = PlayerAccountInfo.Me.playerID;
                    PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, ArcadeStages[one.ID].stageConfig, true);
                }
                newButton.button.onClick.AddListener(LoadThisStage);
                newButton.ID = one.ID;
                newButton.text.text = "Stage" + one.ID;
                newButton.name = "Stage" + one.ID;

                if (one.fightMembers != null)
                {
                    for (int i = 0; i < one.fightMembers.EnemySets.GetValues().Count; i++)
                    {
                        MonsterIconDic.Get(one.fightMembers.EnemySets.GetValues()[i].r_id);
                    }
                    
                    var heroIcons = MemberInfosShow(one.fightMembers.EnemySets.GetValues(), newButton.IconsT);
                    for (var i = 0; i < heroIcons.Count; i++)
                    {
                        HeroIcon heroIcon = heroIcons[i];
                        heroIcon.iconButton.onClick.RemoveAllListeners();
                        heroIcon.iconButton.onClick.AddListener(() => { IconButtonFeature(heroIcon); });
                    }
                    newButton.MemberIcons = heroIcons;
                }
                
                var stageInfo = new StageInfo
                {
                    stageConfig = one,
                    stageButton = newButton
                };
                ArcadeStages.Add(one.ID, stageInfo);
            }
            else
            {
                Debug.Log("重复的Arcade模式关卡ID：" + one.ID);
            }
        }
        StageCount = ArcadeStages.Count;
    }
    
    List<HeroIcon> MemberInfosShow(List<UnitInfo> HeroSets, RectTransform _ShowT)
    {
        foreach (Transform transform in _ShowT)
        {
            Destroy(transform.gameObject);
        }
        List<HeroIcon> icons = new List<HeroIcon>();
        foreach (UnitInfo oneMember in HeroSets)
        {
            icons.Add(HeroIcon.ArrangeHeroIconToT(UnitIconPrefab, oneMember, _ShowT));
        }
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
    
    private Action EndDragExtra;
    
    // 分页排版
    // mode 1: 无限模式 2. 5个关卡算一个章节模式
    void INIPagingSystem(int mode)
    {
        // 假设有100关，然后按钮应该是越往下拖关卡数越大，才能和JumpToNewest()堆起来
        for (int i = StageCount; i > -1; i--)
        {
            if (!ArcadeStages.ContainsKey(i))
            {
                continue;
            }
            ArcadeStages[i].stageButton.gameObject.SetActive(true);
            ArcadeStages[i].stageButton.gameObject.transform.SetParent(ButtonsContainer);
            ArcadeStages[i].stageButton.gameObject.transform.localScale = Vector3.one;
        }
        VerticalLayoutGroup vGroup = ButtonsContainer.GetComponent<VerticalLayoutGroup>();
        ButtonsContainer.sizeDelta = new Vector2(ButtonsContainer.sizeDelta.x, (iconPrefab.button.GetComponent<RectTransform>().rect.height + vGroup.spacing) * ArcadeStages.Count);

        JumpToNewStage.onClick.RemoveAllListeners();
        Action JumpToBtnFeature = () => { };
        if (mode == 1)
        {
            JumpToBtnFeature = () =>
            {
                JumpTo(CurrentTargetScrollbarValue());
            };
            EndDragExtra = NormalAlignment;
        }
        if (mode == 2)
        {
            JumpToBtnFeature = () =>
            {
                JumpTo(PageD(PlayerAccountInfo.Me.ArcadeProcess, StageCount));
            };
            EndDragExtra = FiveSetAlignment;
        }
        JumpToNewStage.onClick.AddListener(JumpToBtnFeature.Invoke);
        RefreshRender();
        JumpToBtnFeature.Invoke();
    }

    void JumpTo(float target)
    {
        DOTween.To(() => _Scrollbar.value, x => _Scrollbar.value = x, target, 0.5f).
            OnComplete((() => { EndDragExtra.Invoke(); }));
    }

    void RefreshRender()
    {
        foreach (KeyValuePair<int, StageInfo> keyValuePair in ArcadeStages)
        {
            Animator btnAnimator = keyValuePair.Value.stageButton.GetComponent<Animator>();
            if (btnAnimator != null)
                btnAnimator.enabled = PlayerAccountInfo.Me.ArcadeProcess + 1 == keyValuePair.Key;
            if (PlayerAccountInfo.Me.ArcadeProcess + 1 >= keyValuePair.Key)
            {
                keyValuePair.Value.ChangeColorOfIcons(true);
            }
            else
            {
                keyValuePair.Value.ChangeColorOfIcons(false);
            }
        }
    }

    // 放在滑动区上
    public void DragEnd()
    {
        JumpTo(PageV(_Scrollbar.value, StageCount));
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