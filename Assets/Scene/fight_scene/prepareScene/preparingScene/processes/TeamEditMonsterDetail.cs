using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamEditMonsterDetail : MainSceneProcess
{
    Transform focusingT;
    public IEnumerator enterProcess()
    {
        yield return (this._MonsterBox.myMonsterBox());
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        focusingT = this._preparingScene.getPosTransform(FloatOnHead.focusingPosNum);
        if (focusingT != null)
        {
            this._CameraManager.Assign_Camera(Camera_Mode_Num.approachToCertainDistance);
            this._CameraManager.current_Camera_Mode.targets = new List<Transform>() {focusingT };
        }
    }
    
    public IEnumerator TeamEditMonsterDetailMonsterIconBehaviour()
    {
        CharacterDataInfo _CharacterDataInfo = this._MemberDetail.focusingCharacterDataInfo;
        if (_CharacterDataInfo == null)
        {
            Debug.Log("严重错误");yield break;
        }
        this._MemberDetail.selfdefindtag.text = _CharacterDataInfo.userd_efined_name;
        UnityEngine.Events.UnityAction definemycharactertag = () =>
        {
            _CharacterDataInfo.userd_efined_name = this._preparingScene._MemberDetail.selfdefindtag.text;
            this._preparingScene.triggerMainProcess(AccountCharsSet.Instance.overrideMyCharsInfo());
        };
        this._MemberDetail.selfdefindtag.onValueChanged.RemoveAllListeners();
        this._MemberDetail.selfdefindtag.onValueChanged.AddListener(delegate { definemycharactertag();});
        this._preparingScene.teamEditPress(_CharacterDataInfo.localID);
        this._preparingScene.triggerPresentationProcess(this._preparingScene.displayMy4V4Team(false, FloatOnHead.focusingPosNum)); //重新更新onSetCharShows 
    }

    public TeamEditMonsterDetail(preparingScene _preparingScene)
    {
        this.step = MainSceneStep.TeamEditMonsterDetail;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
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
        focusingT = null;
    }

    Vector3 rotateTo;
    public override void localUpdate()
    {
        if (focusingT != null)
        {
            focusingT.position = Vector3.Lerp(focusingT.position,
            this._preparingScene.caculateShowModelPosition(new Vector3(0.2f, 0.5f, 10)),2 * Time.deltaTime);//左
            rotateTo = this._CameraManager.transform.position - focusingT.position;
            rotateTo.y = 0;
            focusingT.transform.rotation = Quaternion.LookRotation(rotateTo);
        }
    }
}
