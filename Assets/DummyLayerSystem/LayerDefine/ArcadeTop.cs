using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using mainMenu;
using System;
using DG.Tweening;
using DummyLayerSystem;
using Cocone.ProjectP3;
using Singleton;
using UnityEngine.AddressableAssets;

public partial class ArcadeTop : UILayer
{
    [SerializeField] DedicatedCameraConnector _connector;
    [SerializeField] RectTransform ButtonsContainer;
    [SerializeField] Button JumpToNewStage;
    [SerializeField] ScrollRect _ScrollRect;
    [SerializeField] Scrollbar _Scrollbar;
    [SerializeField] StageButton iconPrefab;
    [SerializeField] HeroIcon UnitIconPrefab;
    [SerializeField] NineForShow _NineForShow;
    
    public static ArcadeTop Open(Action afterAction)
    {
        var returnValue = UILayerLoader.Load(PreScene.target.T,"ArcadeTop") as ArcadeTop;
        returnValue.INIArcadeStageButtons(afterAction);
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("ArcadeTop");
    }
    
    public void IconButtonFeature(HeroIcon heroIcon)
    {
        // 显示模型
        _connector.ShowModel(heroIcon.unitConfig.RECORD_ID);
        // 显示技能组
        _NineForShow.ShowStones_DataInfo(heroIcon.unitInfo);
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
    
    void ShowStages(int stage)
    {
        var showStages = TargetStages(stage);
        foreach (var stageNo in showStages)
        {
            var one = Cach.LoadT<FightInfo>("Stage/" + stageNo);
            one.LoadLocalFightFromScript();
            var newButton = Instantiate(iconPrefab);
            void LoadThisStage()
            {
                one.LoadMyTeam();
                one.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
                PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, one, true);
            }
            newButton.button.onClick.AddListener(LoadThisStage);
            newButton.ID = one.ID;
            newButton.text.text = "Stage" + one.ID;
            newButton.name = "Stage" + one.ID;

            if (one.fightMembers != null)
            {
                for (int i = 0; i < one.fightMembers.EnemySets.GetValues().Count; i++)
                {
                    UnitIconDic.Load(one.fightMembers.EnemySets.GetValues()[i].r_id);
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
        }
    }

    List<int> TargetStages(int stage)
    {
        int Chapter = stage / 5;
        var returnValue = new List<int>()
        {
            Chapter * 5 + 1, Chapter * 5 + 2, Chapter * 5 + 3, Chapter * 5 + 4 ,Chapter * 5 + 5
        };
        return returnValue;
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