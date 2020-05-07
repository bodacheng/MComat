using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using mainMenu;
using dataAccess;

// 先试着把石头添加到一个格子上。
public class TryChangeStonePos : MainSceneProcess
{
    TextAsset TuroialFightScript;
    LocalFight TuroialFight;
    
    public TryChangeStonePos()
    {
        this.Step = MainSceneStep.Tutorial_skillEdit_sub4;
        this.EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        this.TuroialFightScript = Resources.Load("Account/TuroialFight2") as TextAsset;
        if (this.TuroialFightScript != null)
            this.TuroialFight = LocalFight.LoadOneLocalFightByScript(TuroialFightScript);

        void SkillEditConfirm()
        {
            IEnumerator skilleditconfrim()
            {
                IEnumerator getchar = AccountCharsSet.Load("1");
                yield return getchar;
                GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
                yield return TheNineSlot.target.UpdateMyStonesBaseOnSlots(myfighter);
                yield return MemberDetail.target.SkillEditConfirmAnimation();

                StageScriptableObject stage = new StageScriptableObject
                {
                    battleNameCH = "亚当大战傻逼门卫"
                };
                if (TuroialFight != null)
                {
                    CharDataInfo characterDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(myfighter);
                    TuroialFight.HeroSets = new MultiDictionary<int, int, CharDataInfo>();
                    TuroialFight.HeroSets.Set(0, 0, characterDataInfo);
                }
                else
                {
                    Debug.Log("严重错误：未能创建教学战斗信息");
                    yield break;
                }
                stage.localFight = this.TuroialFight;
                stage._fightEventType = FightEventType.Tutorial_Story_AdamVsGuards;
                stage.BattleGroundID = 2;
                PreScene.target.LoadFight(stage);
                ProcessEnd();
            }
            mainProcessRunner.Run(skilleditconfrim());
        }

        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
        yield break;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }
    }}
