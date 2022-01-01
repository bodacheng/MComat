using UnityEngine;
using System.Collections.Generic;
using dataAccess;
using DG.Tweening;
using Skill;
using mainMenu;

public partial class GotchaResultLayer : UILayer
{
    #region 屏幕星星飞入位置
    public RectTransform starWaitPos1, starWaitPos2, starWaitPos3, starWaitPos4, starWaitPos5, starWaitPos6, starWaitPos7, starWaitPos8, starWaitPos9;
    readonly List<RectTransform> waitPos = new List<RectTransform>();
    #endregion
    
    readonly List<Decompositioner> stoneFallingModels = new List<Decompositioner>();
    readonly List<Decompositioner> stoneStartFlashModels = new List<Decompositioner>();
    readonly List<Vector3> slotScreenPos = new List<Vector3>();
    readonly List<Decompositioner> screenStarModels = new List<Decompositioner>();
    readonly List<Decompositioner> screenStarExplosionModels = new List<Decompositioner>();
    
    void SetWaitPos()
    {
        waitPos.Clear();
        waitPos.Add(starWaitPos1);
        waitPos.Add(starWaitPos2);
        waitPos.Add(starWaitPos3);
        waitPos.Add(starWaitPos4);
        waitPos.Add(starWaitPos5);
        waitPos.Add(starWaitPos6);
        waitPos.Add(starWaitPos7);
        waitPos.Add(starWaitPos8);
        waitPos.Add(starWaitPos9);
    }
    
    void StarSortAnim(List<StoneOfPlayerInfo> results)
    {
        for (int i = 0; i < results.Count; i++)
        {
            StarScreenMoveAnim(results[i], PosCal.GetWorldPos(PreScene.target.FxCamera, waitPos[i], 5f), slotScreenPos[i]);
        }
    }
    
    /// <summary>
    /// 一颗星星从屏幕外移动向格子内的动画
    /// </summary>
    /// <param name="info"></param>
    /// <param name="waitPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    void StarScreenMoveAnim(StoneOfPlayerInfo info, Vector3 waitPos, Vector3 endPos)
    {
        SkillConfig skillConfig = SkillConfigTable.GetSkillConfig(info.skillId);
        string screenStarName = "";
        string explosionName = "";
        switch(skillConfig.SP_LEVEL) // 这里应该是rarelevel
        {
            case 0:
                screenStarName = "normal_test_screenstar0";
                explosionName = "screenStarExplostionTest0";
                break;
            case 1:
                screenStarName = "normal_test_screenstar1";
                explosionName = "screenStarExplostionTest1";
                break;
            case 2:
                screenStarName = "normal_test_screenstar2";
                explosionName = "screenStarExplostionTest2";
                break;
            case 3:
                screenStarName = "normal_test_screenstar3";
                explosionName = "screenStarExplostionTest3";
                break;
        }
        
        Decompositioner screenStar = EffectsManager.GenerateEffect(screenStarName, FightGlobalSetting.EffectPathDefine(Zokusei.Null), waitPos, Quaternion.identity, null);
        screenStarModels.Add(screenStar);
        screenStar.transform.DOMove(endPos, 2f).OnComplete(() =>
        {
            Decompositioner effect = EffectsManager.GenerateEffect(explosionName, FightGlobalSetting.EffectPathDefine(Zokusei.Null), endPos, Quaternion.identity, null);
            screenStarExplosionModels.Add(effect);
        });
    }
    
    // 必须使用时候即时运行因为里面几个决定位置的运算要考虑当前相机位置等
    void PosDecide()
    {
        // 星星落入格子
        Vector3 a1ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.A1T.GetComponent<RectTransform>(), 5f);
        Vector3 a2ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.A2T.GetComponent<RectTransform>(), 5f);
        Vector3 a3screenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.A3T.GetComponent<RectTransform>(), 5f);
        Vector3 b1ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.B1T.GetComponent<RectTransform>(), 5f);
        Vector3 b2ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.B2T.GetComponent<RectTransform>(), 5f);
        Vector3 b3ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.B3T.GetComponent<RectTransform>(), 5f);
        Vector3 c1ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.C1T.GetComponent<RectTransform>(), 5f);
        Vector3 c2ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.C2T.GetComponent<RectTransform>(), 5f);
        Vector3 c3ScreenPos = PosCal.GetWorldPos(PreScene.target.FxCamera, NineForShow.C3T.GetComponent<RectTransform>(), 5f);
        slotScreenPos.Clear();
        slotScreenPos.Add(a1ScreenPos);
        slotScreenPos.Add(a2ScreenPos);
        slotScreenPos.Add(a3screenPos);
        slotScreenPos.Add(b1ScreenPos);
        slotScreenPos.Add(b2ScreenPos);
        slotScreenPos.Add(b3ScreenPos);
        slotScreenPos.Add(c1ScreenPos);
        slotScreenPos.Add(c2ScreenPos);
        slotScreenPos.Add(c3ScreenPos);
    }
}
