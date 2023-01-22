using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mainMenu;
using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using ModelView;

public class ArcadeTop : UILayer
{
    [SerializeField] DedicatedCameraConnector connector;
    [SerializeField] VerticalLayoutGroup container;
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
        _cts = cts; 
        nextChapter.onClick.AddListener(ShowNextStages);
        lastChapter.onClick.AddListener(ShowLastStages);
        jumpToNewStage.onClick.AddListener(ToNew);
    }
    
    async UniTask IconButtonFeature(HeroIcon heroIcon)
    {
        // 显示模型
        await connector.ShowModel(heroIcon.unitConfig.RECORD_ID);
        // 显示技能组
        await nineForShow.ShowStones_DataInfo(heroIcon.unitInfo);
        nineForShow.AddOnClickToSlots((RECORD_ID) =>
        {
            var skillConfig = SkillConfigTable.GetSkillConfig(RECORD_ID);
            connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
        });
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
        ProgressLayer.Loading(">");
        container.transform.gameObject.SetActive(false);
        foreach (Transform child in container.transform) {
            Destroy(child.gameObject);
        }
        
        stages.Sort((a, b) => b.CompareTo(a));
        _currentStages = stages;
        async UniTask LoadStage(int stageNo)
        {
            if (token.IsCancellationRequested)
                return;
            
            var one = await AddressablesLogic.LoadT<FightInfo>("Arcade/" + stageNo + ".asset");
            if (one == null)
            {
                return;
            }
            
            var stageButton = Instantiate(iconPrefab, container.transform, true);
            void LoadThisStage()
            {
                one.EventType = FightEventType.Quest;
                if (one.ID == "1")
                {
                    one.RunTutorial = true;
                }
                PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, one, true);
            }
            
            stageButton.button.onClick.AddListener(LoadThisStage);
            stageButton.ID = Convert.ToInt32(one.ID);
            stageButton.text.text = "Stage" + stageNo;
            stageButton.name = "Stage" + stageNo;
            
            if (one.FightMembers != null)
            {
                var heroIcons = await UnitInfosShow(one.FightMembers.EnemySets.GetValues(), stageButton.IconsT);
                for (var i = 0; i < heroIcons.Count; i++)
                {
                    var heroIcon = heroIcons[i];
                    heroIcon.iconButton.onClick.RemoveAllListeners();
                    heroIcon.iconButton.onClick.AddListener( async () =>
                    {
                        ProgressLayer.Loading(">");
                        await IconButtonFeature(heroIcon);
                        ProgressLayer.Close();
                    });
                    // 默认显示章节boss
                    if (stageNo == _currentStages.Max() && i == 0)
                    {
                        await IconButtonFeature(heroIcon);
                    }
                }
                stageButton.MemberIcons = heroIcons;
            }

            var btnAnimator = stageButton.GetComponent<Animator>();
            if (btnAnimator != null)
                btnAnimator.enabled = PlayerAccountInfo.Me.arcadeProcess + 1 == stageNo;
            
            stageButton.ChangeColorOfIcons(PlayerAccountInfo.Me.arcadeProcess + 1 >= stageNo);
            stageButton.transform.localPosition = Vector3.zero;
            stageButton.transform.localRotation = Quaternion.identity;
            stageButton.transform.localScale = Vector3.one;
        }
        var tasks = new List<UniTask>();
        for (var index = 0; index < _currentStages.Count; index++)
        {
            tasks.Add(LoadStage(_currentStages[index]));
        }
        await UniTask.WhenAll(tasks);
        Refresh();
        container.transform.gameObject.SetActive(true);
        ProgressLayer.Close();
    }

    void Refresh()
    {
        nextChapter.gameObject.SetActive(PlayerAccountInfo.Me.arcadeProcess + 1 > _currentStages.Max());
        lastChapter.gameObject.SetActive(_currentStages.Min() > 5);
        
        var progressChapter = PlayerAccountInfo.Me.arcadeProcess / 5;
        var currentChapter = _currentStages.Min() / 5;
        
        jumpToNewStage.gameObject.SetActive(progressChapter != currentChapter);
        
        container.CalculateLayoutInputHorizontal();
        container.CalculateLayoutInputVertical();
        container.SetLayoutHorizontal();
        container.SetLayoutVertical();
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
        List<UniTask> tasks = new List<UniTask>();
        foreach (var unitInfo in heroSets)
        {
            async UniTask load(UnitInfo unitInfo)
            {
                var v = await HeroIcon.ArrangeHeroIconToT(unitIconPrefab, unitInfo, showT);
                icons.Add(v);
            }
            tasks.Add(load(unitInfo));
        }
        await UniTask.WhenAll(tasks);
        for (var i = 0; i < icons.Count; i++)
        {
            icons[i].iconButton.targetGraphic.raycastTarget = true;
        }
        return icons;
    }
}