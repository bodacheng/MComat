using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using ModelView;
using Singleton;

public class ArcadeTop : UILayer
{
    [SerializeField] DedicatedCameraConnector connector;
    [SerializeField] RectTransform container;
    [SerializeField] Button jumpToNewStage;
    [SerializeField] StageButton iconPrefab;
    [SerializeField] HeroIcon unitIconPrefab;
    [SerializeField] NineForShow nineForShow;

    [SerializeField] Button nextChapter;
    [SerializeField] Button lastChapter;

    List<int> _currentStages;
    private CancellationTokenSource _cts;
    public void Setup(CancellationTokenSource cts)
    {
        this._cts = cts; 
        nextChapter.onClick.AddListener(ShowNextStages);
        lastChapter.onClick.AddListener(ShowLastStages);
        jumpToNewStage.onClick.AddListener(ToNew);
    }
    
    void IconButtonFeature(HeroIcon heroIcon)
    {
        // 显示模型
        connector._ShowModel(heroIcon.unitConfig.RECORD_ID).Forget();
        // 显示技能组
        nineForShow.ShowStones_DataInfo(heroIcon.unitInfo);
    }

    void ToNew()
    {
        var stages = NewStages(PlayerAccountInfo.Me.arcadeProcess);
        ShowStages(stages, _cts.Token).Forget();
    }

    void ShowNextStages()
    {
        ShowStages(NewStages(_currentStages.Max() + 1), _cts.Token).Forget();
    }
    
    void ShowLastStages()
    {
        ShowStages(NewStages(_currentStages.Min() - 2), _cts.Token).Forget();
    }
    
    public async UniTask ShowStages(List<int> stages, CancellationToken token)
    {
        foreach (Transform child in container) {
            Destroy(child.gameObject);
        }
        
        stages.Sort((a, b) => b.CompareTo(a));
        _currentStages = stages;
        Refresh();
        for (var index = 0; index < _currentStages.Count; index++)
        {
            if (token.IsCancellationRequested)
                return;
            
            var stageNo = _currentStages[index];
            var one = await AddressablesLogic.LoadT<FightInfo>("Arcade/" + stageNo + ".asset");
            if (one == null)
            {
                continue;
            }
            
            var stageButton = Instantiate(iconPrefab, container, true);
            
            void LoadThisStage()
            {
                one.EventType = FightEventType.Quest;
                if (one.ID == "1")
                {
                    one.runTutorial = true;
                }
                PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, one, true);
            }
            
            stageButton.button.onClick.AddListener(LoadThisStage);
            stageButton.ID = Convert.ToInt32(one.ID);
            stageButton.text.text = "Stage" + stageNo;
            stageButton.name = "Stage" + stageNo;
            
            if (one.FightMembers != null)
            {
                for (var i = 0; i < one.FightMembers.EnemySets.GetValues().Count; i++)
                {
                    await UnitIconDic.Load(one.FightMembers.EnemySets.GetValues()[i].r_id);
                }

                var heroIcons = await UnitInfosShow(one.FightMembers.EnemySets.GetValues(), stageButton.IconsT);
                for (var i = 0; i < heroIcons.Count; i++)
                {
                    var heroIcon = heroIcons[i];
                    heroIcon.iconButton.onClick.RemoveAllListeners();
                    heroIcon.iconButton.onClick.AddListener(() => { IconButtonFeature(heroIcon); });

                    // 默认显示章节boss
                    if (stageNo == _currentStages.Max() && i == 0)
                    {
                        IconButtonFeature(heroIcon);
                    }
                }

                stageButton.MemberIcons = heroIcons;
            }

            var btnAnimator = stageButton.GetComponent<Animator>();
            if (btnAnimator != null)
                btnAnimator.enabled = PlayerAccountInfo.Me.arcadeProcess + 1 == stageNo;
            
            if (PlayerAccountInfo.Me.arcadeProcess + 1 >= stageNo)
            {
                stageButton.ChangeColorOfIcons(true);
            }
            else
            {
                stageButton.ChangeColorOfIcons(false);
            }
            
            stageButton.transform.localPosition = Vector3.zero;
            stageButton.transform.localRotation = Quaternion.identity;
        }
    }

    void Refresh()
    {
        nextChapter.gameObject.SetActive(PlayerAccountInfo.Me.arcadeProcess + 1 > _currentStages.Max());
        lastChapter.gameObject.SetActive(_currentStages.Min() > 5);
        
        var progressChapter = PlayerAccountInfo.Me.arcadeProcess / 5;
        var currentChapter = _currentStages.Min() / 5;
        
        jumpToNewStage.gameObject.SetActive(progressChapter != currentChapter);
    }
    
    public List<int> NewStages(int progress)
    {
        var currentChapter = progress / 5;
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
    
    async UniTask<List<HeroIcon>> UnitInfosShow(List<UnitInfo> heroSets, RectTransform showT)
    {
        foreach (Transform t in showT)
        {
            Destroy(t.gameObject);
        }
        var icons = new List<HeroIcon>();
        foreach (var unitInfo in heroSets)
        {
            var v = await HeroIcon.ArrangeHeroIconToT(unitIconPrefab, unitInfo, showT);
            icons.Add(v);
        }
        for (var i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
}