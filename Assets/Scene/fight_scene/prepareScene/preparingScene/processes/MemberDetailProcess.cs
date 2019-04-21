using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberDetailProcess : MainSceneProcess
{
    RectTransform T;
    
    public MemberDetailProcess(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.MemberDetail;
        this._preparingScene = _preparingScene;
        this.T = T;
        this.EelementsInherit(_preparingScene);
    }
    
    public IEnumerator enterProcess()
    {
        _SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        //foreach (KeyValuePair<int, GameObject> _pair in myModelPool.Instance.ModelDicBasedOnPlayerLocalID)
        //{
        //    _pair.Value.SetActive(false);//这个貌似可以省？
        //}
        this.T.gameObject.SetActive(true);
        
        this._CameraManager.Assign_Camera(Camera_Mode_Num.LockCamera);
        this._CameraManager.current_Camera_Mode.targets = new List<Transform>() { this._MemberDetail.MemDetailWatchPos };

        yield return (this._MonsterBox.myMonsterBox());
        this._MonsterBox.adjustAllIconsSize(-1);
        this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        
        yield return _MemberDetail.refreshMemberDetailGamenSystemBaseOnFocusingChar();
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
         this._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
         this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}
