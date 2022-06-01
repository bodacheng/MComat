using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System;
using System.Linq;
using DummyLayerSystem;
using Cocone.ProjectP3;
using Singleton;

public class ArcadeTop : UILayer
{
    [SerializeField] DedicatedCameraConnector _connector;
    [SerializeField] RectTransform container;
    [SerializeField] Button JumpToNewStage;
    [SerializeField] StageButton iconPrefab;
    [SerializeField] HeroIcon UnitIconPrefab;
    [SerializeField] NineForShow _NineForShow;

    [SerializeField] Button nextChapter;
    [SerializeField] Button lastChapter;

    private List<int> currentStages;
    
    public static ArcadeTop Open()
    {
        var returnValue = UILayerLoader.Load(PreScene.target.T,"ArcadeTop") as ArcadeTop;
        returnValue.nextChapter.onClick.AddListener(returnValue.ShowNextStages);
        returnValue.lastChapter.onClick.AddListener(returnValue.ShowLastStages);
        returnValue.JumpToNewStage.onClick.AddListener(returnValue.ToNew);
        return returnValue;
    }
    
    public static void Close()
    {
        UILayerLoader.Remove("ArcadeTop");
    }
    
    void IconButtonFeature(HeroIcon heroIcon)
    {
        // 显示模型
        _connector.ShowModel(heroIcon.unitConfig.RECORD_ID);
        // 显示技能组
        _NineForShow.ShowStones_DataInfo(heroIcon.unitInfo);
    }

    void ToNew()
    {
        var stages = NewStages(PlayerAccountInfo.Me.ArcadeProcess);
        ShowStages(stages);
    }

    void ShowNextStages()
    {
        ShowStages(NewStages(currentStages.Max() + 1));
    }
    
    void ShowLastStages()
    {
        ShowStages(NewStages(currentStages.Min() - 1));
    }
    
    public void ShowStages(List<int> stages)
    {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }
        
        stages.Sort((a, b) => b.CompareTo(a));
        currentStages = stages;
        for (var index = 0; index < currentStages.Count; index++)
        {
            var stageNo = currentStages[index];
            var one = Cach.LoadT<FightInfo>("Arcade/" + stageNo + ".asset");
            if (one == null)
            {
                continue;
            }
            
            var stageButton = Instantiate(iconPrefab);
            
            void LoadThisStage()
            {
                one.LoadMyTeam();
                one.team1ID = PlayerAccountInfo.Me.PlayFabUsername;
                PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, one, true);
            }
            
            stageButton.button.onClick.AddListener(LoadThisStage);
            stageButton.ID = one.ID;
            stageButton.text.text = "Stage" + stageNo;
            stageButton.name = "Stage" + stageNo;

            if (one.FightMembers != null)
            {
                for (var i = 0; i < one.FightMembers.EnemySets.GetValues().Count; i++)
                {
                    UnitIconDic.Load(one.FightMembers.EnemySets.GetValues()[i].r_id);
                }

                var heroIcons = MemberInfosShow(one.FightMembers.EnemySets.GetValues(), stageButton.IconsT);
                for (var i = 0; i < heroIcons.Count; i++)
                {
                    var heroIcon = heroIcons[i];
                    heroIcon.iconButton.onClick.RemoveAllListeners();
                    heroIcon.iconButton.onClick.AddListener(() => { IconButtonFeature(heroIcon); });

                    // 默认显示章节boss
                    if (stageNo == currentStages.Max() && i == 0)
                    {
                        IconButtonFeature(heroIcon);
                    }
                }

                stageButton.MemberIcons = heroIcons;
            }

            var btnAnimator = stageButton.GetComponent<Animator>();
            if (btnAnimator != null)
                btnAnimator.enabled = PlayerAccountInfo.Me.ArcadeProcess + 1 == stageNo;
            if (PlayerAccountInfo.Me.ArcadeProcess + 1 >= stageNo)
            {
                stageButton.ChangeColorOfIcons(true);
            }
            else
            {
                stageButton.ChangeColorOfIcons(false);
            }

            stageButton.transform.SetParent(container);
            stageButton.transform.localPosition = Vector3.zero;
            stageButton.transform.localRotation = Quaternion.identity;
        }

        Refresh();
    }

    void Refresh()
    {
        nextChapter.gameObject.SetActive(PlayerAccountInfo.Me.ArcadeProcess> currentStages.Max());
        lastChapter.gameObject.SetActive(currentStages.Min() > 5);
        JumpToNewStage.gameObject.SetActive(
            !(PlayerAccountInfo.Me.ArcadeProcess >= currentStages.Min() &&
            PlayerAccountInfo.Me.ArcadeProcess <= currentStages.Max())
        );
    }

    public List<int> NewStages(int stage)
    {
        int currentChapter = stage / 5;
        var returnValue = new List<int>()
        {
            currentChapter * 5 + 1, 
            currentChapter * 5 + 2, 
            currentChapter * 5 + 3, 
            currentChapter * 5 + 4 ,
            currentChapter * 5 + 5
        };
        return returnValue;
    }
    
    List<HeroIcon> MemberInfosShow(List<UnitInfo> HeroSets, RectTransform _ShowT)
    {
        foreach (Transform transform in _ShowT)
        {
            Destroy(transform.gameObject);
        }
        var icons = new List<HeroIcon>();
        foreach (var unitInfo in HeroSets)
        {
            icons.Add(HeroIcon.ArrangeHeroIconToT(UnitIconPrefab, unitInfo, _ShowT));
        }
        for (var i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
}