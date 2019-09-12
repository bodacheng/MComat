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
    
    public TryChangeStonePos(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub4;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }
    
    public IEnumerator enterProcess()
    {
        this.TuroialFightScript = Resources.Load("Account/TuroialFight2") as TextAsset;
        if (this.TuroialFightScript != null)
            this.TuroialFight = LocalFight.loadOneLocalFightByScript(TuroialFightScript);
            
        UnityEngine.Events.UnityAction SkillEditConfirm = () =>//这里可能还有一个执行内容，就是进入到测试战斗场景。
        {
            IEnumerator skilleditconfrim()
            {
                IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo("1");
                yield return getchar;
                GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
                yield return _TheNineSlot.UpdateEditingNineAndTwoBaseOnSlots(myfighter);
                yield return _MemberDetail.SkillEditConfirmAnimation();
                
                StageScriptableObject stage = new StageScriptableObject();
                stage.battleNameCH = "亚当大战傻逼门卫";
                if (this.TuroialFight != null)
                {
                    CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(myfighter);
                    this.TuroialFight.HeroSets = new MultiDictionary<int, int, CharacterDataInfo>();
                    this.TuroialFight.HeroSets.Set(0,0,characterDataInfo);
                }
                else
                {
                    Debug.Log("严重错误：未能创建教学战斗信息");
                    yield break;
                }
                stage.localFight = this.TuroialFight;
                stage._fightEventType = fightEventType.Tutorial_Story_AdamVsGuards;
                stage.BattleGroundID = 2;
                _preparingScene.LoadFight(SceneMode.QuestFight,stage);
                this.ProcessEnd();
            };
            mainProcessRunner.triggerMainProcess(skilleditconfrim());
        };

        UnityEngine.Events.UnityAction SkillUpdateValidation = () =>
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        };
        _TheNineSlot.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        _TheNineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);

        UnityEngine.Events.UnityAction afterEditButtonPressed = () =>
        {
            this._LoadingCanvas.ClearHigtLight();
        };
        yield break;
    }
    
    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void localUpdate()
    {
        if (!this._MemberDetail._SkillsPrintOut.showingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }}
