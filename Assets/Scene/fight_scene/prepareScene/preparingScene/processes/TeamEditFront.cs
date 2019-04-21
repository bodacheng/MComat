using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamEditFront : MainSceneProcess
{
    RectTransform T;
    public IEnumerator enterProcess()
    {
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
        if (this._preparingScene.lastProcess.step == MainSceneStep.TeamEditMonsterDetail)//这个环节是不对的。我们绝不能以这种方式去把step之间建立联系。
        {
            // 缺返回判断
            TeamSet.Instance.overrideTeamSetInfoOnJsonFile();//也就是说只要对队伍进行了一次编辑，立刻保存阵容信息。
        }
        this._CameraManager.Assign_LerpToCertainPlaceCamera(this._preparingScene.TeamEditWatchPoint.position, this._preparingScene.TeamEditWatchPoint.rotation,2f);
        this._preparingScene._SwapAllModelShader.arrangeAllModelShader(-1, myModelPool.Instance.ModelDicBasedOnPlayerLocalID);
        yield return this._preparingScene._MonsterBox.myMonsterBox();
        this._preparingScene.triggerPresentationProcess(this._preparingScene.displayMy4V4Team(false, PosNum.none)); //重新更新onSetCharShows          
        this.T.gameObject.SetActive(true);
        yield break;
    }
    
    public TeamEditFront(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.TeamEditFront;
        this._preparingScene = _preparingScene;
        this.T = T;
        EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this._preparingScene.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
        this._preparingScene.showModelPositionAdjusting();
    }
}
