using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using Api.Dto.Model;

// 游戏第一次开启的时候，出现在玩家眼前九宫格。立刻把游戏的概念给呈现给玩家
// 本process有以下任务
// 1. 读取临时剧情人物并加载
// 2. 读取临时技能石存档并加载
// 3. 显示九宫格，并且安排本流程内九宫格不填满确定按钮就不显示
// 4. 为确定按钮添加直接进入剧情战斗的功能

public class TryEditNineSlot : MainSceneProcess
{
    TextAsset TuroialFightScript;
    LocalFight TuroialFight;
    
    public TryEditNineSlot(ProcessesRunner processesRunner)
    {
        nextProcessStep = MainSceneStep.None;
        SubProcessesRunner = processesRunner;
        EelementsInherit(PreScene.target);
    }

    public override bool CanEnterOtherProcess()
    {
        return false;// nine slot full
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (TheNineSlot.target.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                TheNineSlot.target.A2DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                    TheNineSlot.target.A3DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() &&
                        TheNineSlot.target.B1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                            TheNineSlot.target.B2DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                                TheNineSlot.target.B3DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() &&
                                    TheNineSlot.target.C1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                                        TheNineSlot.target.C2DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                                            TheNineSlot.target.C3DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>())
        {
            if (!TheNineSlot.target.ConfirmSkillChangeButton.gameObject.activeSelf)
            {
                TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(true);
            }
        }else{
            if (TheNineSlot.target.ConfirmSkillChangeButton.gameObject.activeSelf)
            {
                TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(false);
            }
        }
        
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }
    }

    IEnumerator getchar;
    public IEnumerator EnterProcess()
    {
        getchar = AccountCharsSet.Load("1");//测试用角色
        yield return getchar;
        MemberDetail.target.focusingCharDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        yield return SkillEditorButtonBehaviour(MemberDetail.target.focusingCharDataInfo);
        
        TuroialFightScript = Resources.Load("Account/TuroialFight2") as TextAsset;
        if (TuroialFightScript != null)
        {
            TuroialFight = LocalFight.LoadOneLocalFightByScript(TuroialFightScript);
        }
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        
        void SkillEditConfirm()
        {
            IEnumerator skilleditconfrim()
            {
                getchar = AccountCharsSet.Load("1");
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
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
            
        // Tutorial 模式那两按钮不需要显示
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.CurrentMode.target = MemberDetail.target.MemDetailTargetPos;
        
        // 表现系
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(MemberDetail.target.focusingCharDataInfo.monsterId);
        //SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),_CharacterResourceInfo._zokusei);
        yield return RefreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion();
    }
    
    //里面的因数，是剧情人物的角色信息。
    IEnumerator SkillEditorButtonBehaviour(GetMonsterOfPlayerDetailModel _CharDataInfo)
    {
        if (_CharDataInfo == null)
        {
            Debug.Log("没有找到亚当的信息？程序严重错乱");
            yield break;
        }
        yield return TheNineSlot.target.ReadANineAndTwo(_CharDataInfo);
        TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(false);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(true);
        
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(_CharDataInfo.monsterId);
        SkillStonesBox.target.SetFocusingType(_CharacterResourceInfo.TYPE);
        yield return SkillStonesBox.target.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            mainProcessRunner.Run(TheNineSlot.target.UpdateMyStonesBaseOnSlots(_CharDataInfo));
            MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.SkillEditConfirmAnimation());
        }
        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);               
    }
    
    public IEnumerator RefreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion()
    {
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        getchar = AccountCharsSet.Load("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel focusingCharDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        CharDataInfo characterDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(focusingCharDataInfo);
        MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.CharModelAndSkillRenderProcess(characterDataInfo));
        yield break;
    }
}
