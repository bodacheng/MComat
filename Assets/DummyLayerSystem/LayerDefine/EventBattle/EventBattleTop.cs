using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using mainMenu;
using UnityEngine;
using ModelView;

public class EventBattleTop : UILayer
{
    [SerializeField] private DedicatedCameraConnector connector;
    [SerializeField] float cameraConnectorRightSpace = 940;
    [SerializeField] float cameraConnectorVerticalSpace = 150;
    [SerializeField] private NineForShow nineForShow;
    [SerializeField] private EventBattleButton easyModeBtn;
    [SerializeField] private EventBattleButton normalModeBtn;
    [SerializeField] private EventBattleButton hardModeBtn;

    public EventBattleButton EasyModeBtn => easyModeBtn;
    public EventBattleButton NormalModeBtn => normalModeBtn;
    public EventBattleButton HardModeBtn => hardModeBtn;
    
    public void SetupCommon(List<string> completedLevels, FightInfo easyMode, FightInfo normalMode, FightInfo hardMode)
    {
        ResizeCameraConnectorRefLeft(connector.GetComponent<RectTransform>(), cameraConnectorRightSpace, cameraConnectorVerticalSpace);
        
        EasyModeBtn.Setup(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, easyMode, true);
        }, PlayFabReadClient.EventAwards["easy"],  completedLevels.Contains(easyMode.ID), easyMode.team2CGMode);
        
        NormalModeBtn.Setup(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, normalMode, true);
        }, PlayFabReadClient.EventAwards["normal"],  completedLevels.Contains(normalMode.ID), normalMode.team2CGMode);
        
        HardModeBtn.Setup(() =>
        {
            PreScene.target.trySwitchToStep(MainSceneStep.QuestInfo, hardMode, true);
        }, PlayFabReadClient.EventAwards["hard"],  completedLevels.Contains(hardMode.ID), hardMode.team2CGMode);
    }

    public async UniTask IconButtonFeature(UnitInfo unitInfo)
    {
        UnitConfig unitConfig = Units.GetUnitConfig(unitInfo.r_id);
        
        ProgressLayer.Loading(string.Empty);
        BackGroundPS.target.ChangeBGByElement(unitConfig.element);
        
        await UniTask.WhenAll(
            connector.ShowModel(unitConfig.RECORD_ID), 
            nineForShow.SkillSetInfoOfUnitOnArcadePage(unitInfo.set)
        );
        
        nineForShow.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
        ProgressLayer.Close();
    }
}
