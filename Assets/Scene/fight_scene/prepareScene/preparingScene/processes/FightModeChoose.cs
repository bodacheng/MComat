using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightModeChoose : MainSceneProcess
{
    RectTransform T;
    public FightModeChoose(preparingScene _preparingScene,RectTransform T)
    {
        this.step = MainSceneStep.FightModeChoose;
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
        this._LoadingCanvas.DarkOff();
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);
        //在这个环节，stroymode和eventmode的大按钮应该适配一些比较特殊的处理，比如根据玩家上次游玩的关卡定位什么的。。。
        this.T.gameObject.SetActive(true);
        this._LoadingCanvas.LightUp();
    }
    
    public override void ProcessEnd()
    {
        this.T.gameObject.SetActive(false);
    }

    public override void localUpdate()
    {
    }
}
