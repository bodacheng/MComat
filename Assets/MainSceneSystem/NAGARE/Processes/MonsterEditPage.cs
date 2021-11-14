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

    public IEnumerator EnterProcess()
    {
        List<string> CheckIfExceedLimit = SkillStonesBox.CheckIfExceedCellLimit();
        if (CheckIfExceedLimit.Count > 0)
        {
            PreScene.target.trySwitchToStep(MainSceneStep.BoxOverLoadHelper, false);
            yield break;
        }
        skillEditLayer.StonesBox.CellsFeatureLoad(2);
        skillEditLayer.SkillEditButtonFeature(PreScene.target._focusing);
        skillEditLayer.StonesBox._skillStoneDetail.Clear();

        // 没这行的话从技能石升级画面返回的话角色模型加载不出来
        //yield return UnitOptionLayer.target.CharModelRender(UnitInfo.GetCharDataInfo(PreScene.target._focusing));
        
        // 表现系
        CharConfig _CharConfig = MonstersConfigTable.GetCharConfig(PreScene.target._focusing.r_id);
        skillEditLayer.StonesBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons
        (
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.NormalTab.GetComponent<RectTransform>(), 10f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX1Tab.GetComponent<RectTransform>(), 10f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX2Tab.GetComponent<RectTransform>(), 10f),
            ScreenPositionCal.Cal(1, skillEditLayer.StonesBox.fxCamera, skillEditLayer.StonesBox.EX3Tab.GetComponent<RectTransform>(), 10f), 
            _CharConfig._zokusei
        );
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
        skillEditLayer = SkillEditLayer.Open();
        
        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                itemsLoadFinished
            },
            () => {
                if (FightGlobalSetting._programMode == FightGlobalSetting.ProgramMode.skillShow)
                {
                    skillEditLayer.SkillShowSpEnterProcess();
                }
                else
                {
                    mainProcessRunner.RunAsQueued(EnterProcess());
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
        if (!SkillShowSupporter.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
