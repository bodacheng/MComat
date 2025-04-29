using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DummyLayerSystem;
using FightScene;
using UnityEngine;
using UnityEngine.UI;

public class InBattleEvolution : UILayer
{
    [SerializeField] private NineForShow nineForShow;
    [SerializeField] private Text upperText;
    [SerializeField] private Text bottomText;
    [SerializeField] private RectTransform skillOptionsT;
    [SerializeField] private EvolutionSkill[] skillOptions;
    [SerializeField] private GameObject selectedFrame;
    [SerializeField] private float animEndInSeconds = 0.5f;
    [SerializeField] private float animEndOutSeconds = 0.2f;

    [SerializeField] private float atLeastTwoSideSizeInterval = 10;
    
    public async void Setup(Data_Center focusUnit, Action onFinishedSkillEvolution, string upperText, string bottomText)
    {
        this.upperText.text = upperText;
        this.bottomText.text = bottomText;
        
        var set = focusUnit.UnitInfo.set;
        // 取原本的 y，不改它
        float currentY = skillOptionsT.anchoredPosition.y;
        // 新的 X 值
        //这个leftInteral和rightInteral的计算本身其实是没多大正确道理的，它没有考虑不同设备下atLeastTwoSideSizeInterval在实际画面中所占比例的问题
        float leftInteral = Mathf.Max(PosCal.CanvasWidth - PosCal.GetSafeAreaWidthAndHeightInCanvas().Item1, atLeastTwoSideSizeInterval);
        skillOptionsT.anchoredPosition = new Vector2(leftInteral, currentY);

        float rightInteral = Mathf.Min((PosCal.GetSafeAreaWidthAndHeightInCanvas().Item1 - PosCal.CanvasWidth)/2, - atLeastTwoSideSizeInterval);
        var nineSlotRect = nineForShow.transform.GetComponent<RectTransform>();
        nineSlotRect.anchoredPosition = new Vector2(rightInteral, currentY);
        
        var leftMaxWidthForNineSlots = PosCal.GetSafeAreaWidthAndHeightInCanvas().Item1 -
                                       skillOptionsT.rect.width - leftInteral + rightInteral;
        float nineSlotWidth = MathF.Min(leftMaxWidthForNineSlots, nineSlotRect.rect.height);
        nineSlotRect.sizeDelta = new Vector2(nineSlotWidth, nineSlotWidth);
        float cellSize = nineSlotWidth / 3;
        var gridLayoutGroup = nineForShow.transform.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize); 
        
        nineForShow.AddOnClickToSlots(
            (BOButton btn) =>
            {
                selectedFrame.SetActive(true);
                selectedFrame.transform.SetParent(btn.GetComponent<RectTransform>());
                selectedFrame.transform.localPosition = Vector3.zero;
                selectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                selectedFrame.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                selectedFrame.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                selectedFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize, cellSize);
                selectedFrame.gameObject.SetActive(true);
            }
        );
        
        var recommendedTargetReplaceSlot = 
            focusUnit.UnitInfo.set.RecommendedTargetReplaceSlot
                (RTFightManager.Target.EvolutionManager.EvolutionCount >= 3);
        nineForShow.ClickTargetSlot(recommendedTargetReplaceSlot);
        
        await UniTask.WhenAll(
            nineForShow.ShowStones(
                set.a1, set.a2, set.a3,
                set.b1, set.b2, set.b3,
                set.c1, set.c2, set.c3
            ),
            ShowSkillsToChoose(focusUnit, onFinishedSkillEvolution, cellSize)
        );
    }
    
    async UniTask ShowSkillsToChoose(Data_Center focusUnit, Action onFinishedSkillEvolution, float stoneSize)
    {
        nineForShow.EvolutionModeSlotInteractiveRefresh(focusUnit.UnitInfo.set, RTFightManager.Target.EvolutionManager.EvolutionCount >= 3);
        await nineForShow.RefreshEffects(FightScene.FightScene.target.fxCamera, stoneSize / 150f);
        var skills = RTFightManager.Target.EvolutionManager.RandomSkillList("human", focusUnit.UnitInfo.set);

        for (var i = 0; i < skillOptions.Length; i++)
        {
            // 1) 先移除旧的所有监听，防止重复注册
            skillOptions[i].Btn.onClick.RemoveAllListeners();
            skillOptions[i].ShowIcon(skills[i], stoneSize);
        }
        
        await UniTask.Delay(TimeSpan.FromMilliseconds(350), ignoreTimeScale: true);//这个的目的不是演出，是因为一个技能选择画面出现后急速选择一个技能的情况下可能导致程序崩溃的bug。我们没能查到bug原因。
        
        for (var i = 0; i < skillOptions.Length; i++)
        {
            var index = i;
            skillOptions[i].Btn.SetListener(
                async () =>
                {
                    for (var a = 0; a < skillOptions.Length; a++)
                    {
                        skillOptions[a].Btn.onClick.RemoveAllListeners();
                        if (a != index)
                        {
                            skillOptions[a].Animator.SetTrigger("fade");
                        }
                    }
                    
                    await RTFightManager.Target.EvolutionManager.ChangeSkill(focusUnit, nineForShow.ClickedSlot, skills[index]);
                    var t = skillOptions[index].Btn.transform.GetComponentInChildren<SKStoneItem>();
                    var clickedSlot = nineForShow.GetClickedSlot();
                    var moveTween = t.transform.DOMove(clickedSlot.transform.position, animEndInSeconds).SetEase(Ease.InBack).OnComplete(
                        async () =>
                        {
                            var stone = clickedSlot.transform.GetComponentInChildren<SKStoneItem>();
                            if (stone != null)
                                stone.gameObject.SetActive(false);
                            
                            var targetPos = PosCal.GetWorldPos(FightScene.FightScene.target.fxCamera, clickedSlot.transform.GetComponent<RectTransform>(), 7);
                            var _layer = UILayerLoader.Get<FightingStepLayer>();
                            var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(skills[index]);
                            var explosion = _layer.InputsManager.GetCurrentElementEffectsGroup().GetExplosionEffect(skillConfig.SP_LEVEL);
                            explosion.transform.position = targetPos;
                            explosion.transform.localScale *= 3;
                            explosion.Play();
                            await UniTask.Delay(TimeSpan.FromSeconds(animEndOutSeconds));
                            explosion.transform.localScale /= 3;
                            onFinishedSkillEvolution.Invoke();
                        }
                    );
                }
            );
        }
    }
}
