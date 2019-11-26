using System.Collections;
using Api.Dto.Model;
using UnityEngine;
using mainMenu;
using dataAccess;

// 先试着把石头添加到一个格子上。
public class TryEditALines : MainSceneProcess
{
    StageScriptableObject _StageScriptableObject;
    LocalFight TuroialFight;
    int step = 1;
    public IEnumerator EnterProcess()
    {
        step = 1;
        TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(false);
        TheNineSlot.Instance.A2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.A3DragAndDropCell.gameObject.SetActive(true);
        //this._TheNineSlot.B1DragAndDropCell.gameObject.SetActive(true);
        //this._TheNineSlot.B2DragAndDropCell.gameObject.SetActive(true);
        //this._TheNineSlot.B3DragAndDropCell.gameObject.SetActive(true);
        //this._TheNineSlot.C1DragAndDropCell.gameObject.SetActive(true);
        //this._TheNineSlot.C2DragAndDropCell.gameObject.SetActive(true);
        //this._TheNineSlot.C3DragAndDropCell.gameObject.SetActive(true);
    
        _StageScriptableObject = Resources.Load("Account/TuroialFight1") as StageScriptableObject;
        if (_StageScriptableObject != null)
            TuroialFight = LocalFight.loadOneLocalFightByScript(_StageScriptableObject.Script);
            
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
        _MemberDetail.focusingCharacterDataInfo = myfighter;
        yield return SkillEditorButtonBehaviour(_MemberDetail.focusingCharacterDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。        
        yield break;
    }
    
    public TryEditALines(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub2;
        this._preparingScene = _preparingScene;       
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return int.Parse(_MemberDetail.focusingCharacterDataInfo.a1_skill_stone_record_id) != -1 &&
                int.Parse(_MemberDetail.focusingCharacterDataInfo.a2_skill_stone_record_id) != -1 &&
                    int.Parse(_MemberDetail.focusingCharacterDataInfo.a3_skill_stone_record_id) != -1;
    }

    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    public override void LocalUpdate()
    {
        if (step == 1)
        {
            if (TheNineSlot.Instance.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() &&
                TheNineSlot.Instance.A2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() &&
                    TheNineSlot.Instance.A3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>())
            {
                TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(true);
                step = 2;
            }
        }
    }
    
        //里面的因数，是剧情人物“亚当”的角色信息。
    IEnumerator SkillEditorButtonBehaviour(GetMonsterOfPlayerDetailModel _CharacterDataInfo)
    {
        if (_CharacterDataInfo == null)
        {
            Debug.Log("没有找到亚当的信息？程序严重错乱");
            yield break;
        }
        yield return TheNineSlot.Instance.ReadANineAndTwo(_CharacterDataInfo);
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(_CharacterDataInfo.monsterId);
        SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        yield return (SkillStonesBox.Instance.EXTabsFeatureRefresh(false));
        void SkillEditConfirm()
        {
            mainProcessRunner.triggerMainProcess(TheNineSlot.Instance.UpdateEditingNineAndTwoBaseOnSlots(_CharacterDataInfo));
            _MemberDetail.presentationProcessRunner.triggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
            StageScriptableObject stage = new StageScriptableObject
            {
                battleNameCH = "亚当大战傻逼门卫"
            };
            if (this.TuroialFight != null)
            {
                CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(_CharacterDataInfo);
                this.TuroialFight.HeroSets = new MultiDictionary<int, int, CharacterDataInfo>();
                this.TuroialFight.HeroSets.Set(0, 0, characterDataInfo);
            }
            else
            {
                Debug.Log("严重错误：未能创建教学战斗信息");
                return;
            }
            stage.localFight = this.TuroialFight;
            stage._fightEventType = fightEventType.Tutorial_Basic;
            stage.BattleGroundID = 2;
            _preparingScene.LoadFight(SceneMode.QuestFight, stage);
            this.ProcessEnd();
        }

        void SkillUpdateValidation()
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
}
