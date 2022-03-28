using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using DummyLayerSystem;
using PlayFab.ClientModels;

public class FrontPage : MainSceneProcess
{
    void UserDataLoadFinished(bool value)
    {
        missionWatcher.Finish("userDataLoadFinished", value);
    }
    
    void UserReadOnlyDataLoadFinished(bool value)
    {
        missionWatcher.Finish("userReadOnlyDataLoadLoaded", value);
    }
    
    void StatisticsLoadFinished(bool value)
    {
        missionWatcher.Finish("statisticsFinished", value);
    }
    
    void ItemsLoadFinished(bool value)
    {
        missionWatcher.Finish("itemsLoadFinished", value);
    }
    
    void ArenaTFinished(bool value)
    {
        missionWatcher.Finish("arenaTFinished", value);
    }
    
    void ArcadeTFinished(bool value)
    {
        missionWatcher.Finish("arcadeTFinished", value);
    }

    public FrontPage()
    {
        Step = MainSceneStep.FrontPage;
        Inherit(PreScene.target);
    }

    FrontLayer frontLayer;
    IEnumerator EnterProcess()
    {
        frontLayer = UILayerLoader.Load(PreScene.target.T, "FrontLayer") as FrontLayer;
        frontLayer.Initialise(PreScene.target);
        
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.2f, 0.1f);
        //MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(PreScene.target.MemDetailWatchPos.position, PreScene.target.MemDetailTargetPos, 3f, 15f);
        
        var focus_instanceID = TeamSet.Default.GetInstanceIdOnPos(0);
        if (focus_instanceID == null)
        {
            foreach (var keyValuePair in MyMonsters.Dic)
            {
                focus_instanceID = keyValuePair.Key;
                break;
            }
        }
        PreScene.target.SetFocusingUnit(focus_instanceID);//确立focusing角色
        yield return ModelShower.target.ShowMyModel(focus_instanceID);
        
        UpperInfoBar.Open(() => PreScene.target.trySwitchToStep(MainSceneStep.Setting, true), 
            () => PreScene.target.trySwitchToStep(MainSceneStep.MailBox));
    }
    
    public override void ProcessEnter()
    {
        PopupLayer.Loading(">", PreScene.target.T);
        PlayFabReadClient.GetUserData(
            new GetUserDataRequest
            {
                PlayFabId = PlayerAccountInfo.Me.PlayFabUsername,
                Keys = new List<string>() { "PlayerName" }
            }, UserDataLoadFinished);
        PlayFabReadClient.GetUserReadOnlyData(UserReadOnlyDataLoadFinished);
        PlayFabReadClient.GetStatistics(StatisticsLoadFinished);
        
        //AccountCharsSet.LoadTutorial();
        PlayFabReadClient.LoadItems(ItemsLoadFinished);
        PlayFabReadClient.LoadTeamSet("arena", ArenaTFinished);
        PlayFabReadClient.LoadTeamSet("arcade", ArcadeTFinished);
        
        missionWatcher = new MissionWatcher(
            new List<string>
            {
                "userDataLoadFinished", "itemsLoadFinished", "statisticsFinished", 
                "userReadOnlyDataLoadLoaded", "arcadeTFinished", "arenaTFinished"
            },
            () =>
            {
                PopupLayer.Close();
                mainProcessRunner.RunAsQueued(EnterProcess());
            },
            () => { Debug.Log("错误，怎么办？"); }
        );
    }
    
    public override void ProcessEnd()
    {
        if (frontLayer != null)
            GameObject.Destroy(frontLayer.gameObject);
        
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        UpperInfoBar.Close();
        
        SingleThreadProcesser.backup.RunAsQueued(ModelShower.target.ShowMyModel(null));
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.22f, 10);
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
