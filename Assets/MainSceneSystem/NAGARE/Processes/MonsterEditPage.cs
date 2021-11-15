using System.Collections;
using UnityEngine;
using mainMenu;
using System.Collections.Generic;
using UniRx;

public class MonsterEditPage : MainSceneProcess
{
    private SkillEditLayer skillEditLayer;

    ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }

    void EnterProcess()
    {
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            return;
        }
        skillEditLayer = SkillEditLayer.Open();
        
        // 没这行的话从技能石升级画面返回的话角色模型加载不出来
        //yield return UnitOptionLayer.target.CharModelRender(UnitInfo.GetCharDataInfo(PreScene.target._focusing));
    }
    
    public MonsterEditPage()
    {
        Step = MainSceneStep.UnitSkillEdit;
        EelementsInherit(PreScene.target);
    }
    
    //private StoneListSideLayer StoneListSideLayer;
    public override void ProcessEnter()
    {
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        
        //StoneListSideLayer = StoneListSideLayer.Open();
        
        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                itemsLoadFinished
            },
            () => {
                if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
                {
                    skillEditLayer = SkillEditLayer.Open();
                    skillEditLayer.SkillShowSpEnterProcess();
                }
                else
                {
                    EnterProcess();
                }
            },
            () => { Debug.Log("failed"); }
        );
    }
    
    public override void ProcessEnd()
    {
        SkillEditLayer.Close();
        ItemsLoadFinished(0);
        missionWatcher.DisposeAll();
        skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.CloseShowingZokuseiTagEffects();
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        SkillShowSupporter.SkillsPrintOutLateUpdate();
        
        if (!SkillShowSupporter.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
