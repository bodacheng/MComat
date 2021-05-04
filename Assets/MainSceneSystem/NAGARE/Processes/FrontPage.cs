using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;
using DG.Tweening;
using System.Collections.Generic;
using UniRx;
using Api.Dto.Model;

public class FrontPage : MainSceneProcess
{
    public FrontPage()
    {
        Step = MainSceneStep.FrontPage;
        EelementsInherit(PreScene.target);
    }

    // 读取角色列表完成判断
    ReactiveProperty<int> accLoadFinished = new ReactiveProperty<int>(0);
    void AccLoadFinished(int value)
    {
        accLoadFinished.Value = value;
    }

    ReactiveProperty<int> monsterLoadFinished = new ReactiveProperty<int>(0);
    void MonsterLoadFinished(int value)
    {
        monsterLoadFinished.Value = value;
    }

    public IEnumerator EnterProcess()
    {
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 2.2f, 0.1f);
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);

        string focusLocalid = TeamSet.Default.GetMonsterOfPlayerIdOnPos(0);
        if (focusLocalid == null)
        {
            foreach (KeyValuePair<string, MonsterOfPlayerDetailModel> keyValuePair in AccountCharsSet.AccountCharInfoDic)
            {
                focusLocalid = keyValuePair.Key;
                break;
            }
        }
        yield return MemberDetail.target.SetMemberDetailFocusingChar(focusLocalid);//确立focusing角色
        yield return ModelShower.target.ShowMyModel(focusLocalid);
        MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(true);
        UpperInfoBar.target.T.gameObject.SetActive(true);
    }
    
    public override void ProcessEnter()
    {
        AccountSet.LoadCustomerInfo(AccLoadFinished);
        switch (AccountSet._AccInfo.accountprogress)
        {
            case PlayerAccountProgressStep.Freedom:
                AccountCharsSet.Load_List(MonsterLoadFinished);
                break;
            case PlayerAccountProgressStep.justCreated:
                break;
            case PlayerAccountProgressStep.Tutorial:
                AccountCharsSet.LoadTutorial();
                break;
        }
        MissionWatcher missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                accLoadFinished, monsterLoadFinished
            },
            () => mainProcessRunner.RunAsQueued(EnterProcess()),
            () => { Debug.Log("错误，怎么办？"); }
        );
    }
    
    public override void ProcessEnd()
    {
        DOTween.To(() => CameraManager._camera.orthographicSize, x => CameraManager._camera.orthographicSize = x, 3f, 0.1f);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
        UpperInfoBar.target.T.gameObject.SetActive(false);
        BigButtonRender.target.TestOff();
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.22f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
